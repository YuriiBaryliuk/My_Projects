import asyncio
import httpx
import json
import time
import logging
import re
from urllib.parse import urljoin
from datetime import datetime
from bs4 import BeautifulSoup

# ====================== LOGGER ======================
logging.basicConfig(
    level=logging.INFO,
    format='%(asctime)s - %(levelname)s - %(message)s'
)
logger = logging.getLogger(__name__)

# ====================== CONFIG ======================
BASE_URL = "https://www.naturalex.shop"
HEADERS = {
    "User-Agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36"
}

CATEGORY_URL = "https://www.naturalex.shop/cosmetique-hygiene-c102x4343963"

MAX_CONCURRENT = 15
DELAY = 0.15


async def fetch_page(client: httpx.AsyncClient, url: str, retries: int = 3):
    for attempt in range(retries):
        try:
            response = await client.get(url, timeout=20.0)
            response.raise_for_status()
            content = response.content
            try:
                return content.decode('utf-8')
            except UnicodeDecodeError:
                return content.decode('windows-1250', errors='replace')
        except Exception as e:
            if attempt == retries - 1:
                logger.error(f"Failed to fetch {url}: {e}")
                return None
            await asyncio.sleep(2 ** attempt)
    return None


def get_category_product_urls(category_url: str) -> list[str]:
    logger.info(f"Crawling category: {category_url}")
    product_urls: list[str] = []
    seen: set[str] = set()

    try:
        r = httpx.get(category_url, headers=HEADERS, timeout=30.0)
        r.raise_for_status()
        html = r.content.decode('utf-8', errors='replace')
    except Exception as e:
        logger.error(f"Could not fetch category page: {e}")
        return []

    def extract_links(html: str) -> None:
        soup = BeautifulSoup(html, 'html.parser')
        for a in soup.find_all('a', href=True):
            href = a['href']
            if re.search(r'-c2x\d+$', href):
                full = urljoin(BASE_URL, href)
                if full not in seen:
                    seen.add(full)
                    product_urls.append(full)

    def get_total_pages(html: str) -> int:
        pages = re.findall(r'[?&]page=(\d+)', html)
        if pages:
            return max(int(p) for p in pages)
        pages = re.findall(r'GoToPage\((\d+)\)', html)
        return max((int(p) for p in pages), default=1)

    extract_links(html)
    total_pages = get_total_pages(html)
    logger.info(f"Category has {total_pages} page(s), {len(product_urls)} products on page 1")

    for page in range(2, total_pages + 1):
        url = f"{category_url}?page={page}"
        logger.info(f"  Category page {page}/{total_pages}  ({len(product_urls)} products so far)")
        try:
            r = httpx.get(url, headers=HEADERS, timeout=30.0)
            r.raise_for_status()
            extract_links(r.content.decode('utf-8', errors='replace'))
        except Exception as e:
            logger.warning(f"Failed to fetch page {page}: {e}")
        time.sleep(0.3)

    logger.info(f"Found {len(product_urls)} product URLs in category")
    return product_urls


def extract_jsonld_product(soup) -> dict | None:
    """
    Return the parsed Product JSON-LD block for this page, if present.

    This is the authoritative source for sku, name, description, brand and
    gtin13 — it's structured data the site itself publishes, so it's far
    more reliable than scraping it back out of rendered HTML.
    """
    for script in soup.find_all("script", type="application/ld+json"):
        if not script.string:
            continue
        try:
            data = json.loads(script.string)
        except (json.JSONDecodeError, TypeError):
            continue
        if isinstance(data, dict) and data.get("@type") == "Product":
            return data
    return None


def clean_title(raw: str) -> str:
    if not raw:
        return raw
    # Strip a trailing "*" / "**" footnote marker some titles carry
    raw = re.sub(r'\*+\s*$', '', raw.strip())
    return re.sub(r'\s+', ' ', raw).strip()


def extract_product_data(html: str, url: str):
    soup = BeautifulSoup(html, 'html.parser')
    jsonld = extract_jsonld_product(soup)

    # Article ID (still only reliably available from the URL)
    article_match = re.search(r'-c2x(\d+)', url)
    article_id = article_match.group(1) if article_match else None
    if not article_id:
        return None, None

    # --- Title: prefer JSON-LD "name", fall back to <h1>/<title> ---
    if jsonld and jsonld.get("name"):
        title = clean_title(jsonld["name"])
    else:
        title_tag = soup.find('h1') or soup.find('title')
        title = title_tag.get_text(strip=True) if title_tag else None
        if title:
            title = re.split(r'\s*-\s*(Naturalex|www\.naturalex\.shop)', title, flags=re.I)[0].strip()
            title = re.sub(r'\s+', ' ', title).strip()

    # --- SKU: prefer JSON-LD "sku", fall back to "(Code: ...)" scrape ---
    sku = (jsonld.get("sku") if jsonld else None) or extract_sku(html)

    # --- GTIN13: from JSON-LD ---
    gtin13 = (jsonld.get("gtin13") if jsonld else "") or ""

    # --- Vendor: prefer JSON-LD "brand.name", fall back to title regex ---
    vendor = None
    if jsonld and isinstance(jsonld.get("brand"), dict):
        vendor = jsonld["brand"].get("name")
    if not vendor:
        vendor = extract_vendor(title, soup)

    # --- Description: prefer JSON-LD "description" (full HTML incl. ---
    # --- ingredients/origin), fall back to scraped span/td ---
    if jsonld and jsonld.get("description"):
        description_html = jsonld["description"]
    else:
        desc = (soup.find('span', class_=re.compile('PBItemDesc')) or
                soup.find('td', colspan=True))
        description_html = str(desc) if desc else None

    # --- Price: meta tag first, JSON-LD offers.price as fallback ---
    price_tag = soup.find('meta', itemprop='price')
    meta_price = float(price_tag['content']) if price_tag and price_tag.get('content') else 0.0
    if meta_price == 0.0 and jsonld and isinstance(jsonld.get("offers"), dict):
        try:
            meta_price = float(jsonld["offers"].get("price", 0) or 0)
        except (TypeError, ValueError):
            meta_price = 0.0

    strike_tag = soup.find('div', class_='PBStrike')
    compare_price = 0.0
    if strike_tag:
        raw = re.sub(r'[^\d,.]', '', strike_tag.get_text())
        raw = raw.replace(',', '.')
        try:
            compare_price = float(raw)
        except ValueError:
            compare_price = 0.0

    if meta_price == 0.0:
        final_cost = None
        final_compare = None
    elif compare_price > 0:
        final_cost = meta_price
        final_compare = compare_price
    else:
        final_cost = None
        final_compare = meta_price

    # Images
    images = []
    for img in soup.find_all('img'):
        src = img.get('src') or img.get('data-src')
        if src and any(ext in src.lower() for ext in ['.jpg', '.jpeg', '.png', '.webp']):
            full_url = urljoin(BASE_URL, src)
            if full_url not in images and 'logo' not in full_url.lower():
                images.append(full_url)
    images_str = ';'.join(images[:10])

    # Tags
    tags = []
    meta_keywords = soup.find('meta', attrs={'name': 'keywords'})
    if meta_keywords and meta_keywords.get('content'):
        keywords = meta_keywords['content'].strip()
        tags = [tag.strip() for tag in keywords.split(',') if tag.strip()]
    tags_str = ",".join(tags)

    product = {
        "article_id": article_id,
        "title": title,
        "descriptionHtml": description_html,
        "productType": "Cosmétique & Hygiène",
        "vendor": vendor,
        "images": images_str,
        "video_originalSource": "",
        "seo_title": title,
        "tags": tags_str,
        "option_names": ["Title"],
        "metafields": []
    }

    variant = {
        "article_id": article_id,
        "SKU": sku,
        "barcode": gtin13,
        "title": "Default Title",
        "cost": final_cost,
        "compareAtPrice": final_compare,
        "availableQuantity": 0,
        "media_originalSource": images[0] if images else "",
        "inventoryPolicy": "DENY",
        "taxable": True,
        "option": ["Default Title"],
        "metafields": []
    }

    return product, variant


def extract_vendor(title: str, soup) -> str:
    vendor = None
    if title:
        # Look for common brand patterns after the first comma
        match = re.search(r',\s*([^,*]+?)(?:\s*,\s*|\s*\*|$)', title.strip(), re.I)
        if match:
            vendor = match.group(1).strip()
    return vendor or ""


def extract_sku(html):
    if not html or not isinstance(html, str):
        return None
    match = re.search(r'(?:\(Code:|\bCode:)\s*([A-Z0-9-]+)', html, re.I)
    return match.group(1).strip() if match else None


async def scrape_product(client: httpx.AsyncClient, url: str):
    if not re.search(r'-c2x\d+', url):
        return None, None

    logger.info(f"Scraping → {url.split('-c2x')[-1]}")

    html = await fetch_page(client, url)
    if not html:
        return None, None

    try:
        return extract_product_data(html, url)
    except Exception as e:
        logger.error(f"Error parsing {url}: {e}")
        return None, None


async def main():
    start_time = time.time()
    logger.info("Starting — single category mode")

    product_urls = get_category_product_urls(CATEGORY_URL)

    if not product_urls:
        logger.error("No product URLs found. Exiting.")
        return

    products = []
    variants = []
    seen = set()

    async with httpx.AsyncClient(headers=HEADERS, timeout=30.0,
                                  limits=httpx.Limits(max_connections=20)) as client:
        semaphore = asyncio.Semaphore(MAX_CONCURRENT)

        async def bounded_scrape(url):
            async with semaphore:
                result = await scrape_product(client, url)
                await asyncio.sleep(DELAY)
                return result

        results = await asyncio.gather(*[bounded_scrape(url) for url in product_urls])

        for prod, var in results:
            if prod and prod.get("article_id") and prod["article_id"] not in seen:
                seen.add(prod["article_id"])
                products.append(prod)
                if var:
                    variants.append(var)

    filename = f"naturalex_cosmetique.json"

    with open(filename, 'w', encoding='utf-8') as f:
        json.dump({"products": products, "variants": variants}, f,
                  ensure_ascii=False, indent=2)

    total_time = time.time() - start_time
    logger.info(f"✅ {len(products)} products saved to {filename}")
    logger.info(f"Total time: {total_time:.1f}s ({total_time / 60:.1f} min)")


if __name__ == "__main__":
    asyncio.run(main())
