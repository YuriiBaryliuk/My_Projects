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

MAX_CONCURRENT = 15  # You can increase this (async is more efficient)
DELAY = 0.15  # Small delay between requests


async def fetch_page(client: httpx.AsyncClient, url: str, retries: int = 3):
    for attempt in range(retries):
        try:
            response = await client.get(url, timeout=20.0)
            response.raise_for_status()
            content = response.content  # raw bytes
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


def get_all_product_urls():
    """Synchronous because sitemap is fetched only once"""
    logger.info("Fetching sitemap...")

    try:
        r = httpx.get(f"{BASE_URL}/Sitemap.asp?AccID=113973&LangID=0",
                      headers=HEADERS, timeout=30.0)
        r.raise_for_status()
        r.encoding = 'utf-8'
        xml = r.text
    except Exception as e:
        logger.error(f"Sitemap fetch failed: {e}")
        return []

    soup = BeautifulSoup(xml, 'xml')
    urls = [loc.text.strip() for loc in soup.find_all('loc')]
    product_urls = [u for u in urls if re.search(r'-c2x\d+', u)]

    logger.info(f"Found {len(product_urls)} product URLs")
    return product_urls


def extract_sku(html):
    if not html or not isinstance(html, str):
        return None
    match = re.search(r'(?:\(Code:|\bCode:)\s*([A-Z0-9-]+)', html, re.I)
    return match.group(1).strip() if match else None

def extract_gtin13(soup):
    for script in soup.find_all("script", type="application/ld+json"):
        try:
            data = json.loads(script.string)
        except (json.JSONDecodeError, TypeError):
            continue
        if isinstance(data, dict) and data.get("@type") == "Product":
            gtin = data.get("gtin13")
            if gtin:
                return gtin
    return ""

def extract_product_data(html: str, url: str):
    soup = BeautifulSoup(html, 'html.parser')

    # Title
    title_tag = soup.find('h1') or soup.find('title')
    title = title_tag.get_text(strip=True) if title_tag else None
    if title and " - " in title:
        title = title.split(" - ")[0]

    # Article ID
    article_match = re.search(r'-c2x(\d+)', url)
    article_id = article_match.group(1) if article_match else None
    if not article_id:
        return None, None

    sku = extract_sku(html)
    gtin13 = extract_gtin13(soup)

    # Vendor
    vendor = None
    if title:
        v_match = re.search(r',\s*([^,]+?)\s*,\s*\d+[gmlkg]', title, re.I)
        if v_match:
            vendor = v_match.group(1).strip()

    # Product Type
    product_type = None
    for link in soup.find_all('a', href=True):
        if 'c102x' in link.get('href', ''):
            cat = link.get_text(strip=True)
            if cat and len(cat) > 4 and "continuer" not in cat.lower():
                product_type = cat
                break

    # Description
    desc = (soup.find('span', class_=re.compile('PBItemDesc')) or
            soup.find('td', colspan=True))
    description_html = str(desc) if desc else None

    # Images
    images = []
    for img in soup.find_all('img'):
        src = img.get('src') or img.get('data-src')
        if src and any(ext in src.lower() for ext in ['.jpg', '.jpeg', '.png', '.webp']):
            full_url = urljoin(BASE_URL, src)
            if full_url not in images and 'logo' not in full_url.lower():
                images.append(full_url)
    images_str = ';'.join(images[:10])

    # Tags from meta keywords
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
        "productType": product_type,
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
        "cost": 0.0,
        "compareAtPrice": 0.0,
        "availableQuantity": 0,
        "media_originalSource": images[0] if images else "",
        "inventoryPolicy": "DENY",
        "taxable": True,
        "option": ["Default Title"],
        "metafields": []
    }

    return product, variant


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
    logger.info("Starting...")

    product_urls = get_all_product_urls()
    if not product_urls:
        logger.error("No product URLs found. Exiting.")
        return

    products = []
    variants = []
    seen = set()

    # Create async HTTP client
    async with httpx.AsyncClient(headers=HEADERS, timeout=30.0, limits=httpx.Limits(max_connections=20)) as client:
        # Create tasks
        tasks = [scrape_product(client, url) for url in product_urls]

        # Process with semaphore to control concurrency
        semaphore = asyncio.Semaphore(MAX_CONCURRENT)

        async def bounded_scrape(url):
            async with semaphore:
                result = await scrape_product(client, url)
                await asyncio.sleep(DELAY)  # Politeness delay
                return result

        # Run all tasks
        results = await asyncio.gather(*[bounded_scrape(url) for url in product_urls])

        # Process results
        for prod, var in results:
            if prod and prod.get("article_id") and prod["article_id"] not in seen:
                seen.add(prod["article_id"])
                products.append(prod)
                if var:
                    variants.append(var)

    # Save to JSON
    timestamp = datetime.now().strftime("%Y%m%d_%H%M%S")
    filename = f"naturalex_all_products_{timestamp}.json"

    data = {"products": products, "variants": variants}

    with open(filename, 'w', encoding='utf-8') as f:
        json.dump(data, f, ensure_ascii=False, indent=2)

    total_time = time.time() - start_time
    logger.info(f"{len(products)} products saved to {filename}")
    logger.info(f"Total time: {total_time:.1f} seconds ({total_time / 60:.1f} minutes)")


if __name__ == "__main__":
    asyncio.run(main())
