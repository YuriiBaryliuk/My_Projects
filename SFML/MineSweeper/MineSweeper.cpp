#include <SFML/Graphics.hpp>
#include <time.h>
#include <iostream>

using namespace sf;

void numberFiller(int gridLogic[12][12]) {
    for (int i = 1; i <= 10; i++)
        for (int j = 1; j <= 10; j++)
        {
            int n = 0;
            if (gridLogic[i][j] == 9) continue;
            if (gridLogic[i + 1][j] == 9) n++;
            if (gridLogic[i][j + 1] == 9) n++;
            if (gridLogic[i - 1][j] == 9) n++;
            if (gridLogic[i][j - 1] == 9) n++;
            if (gridLogic[i + 1][j + 1] == 9) n++;
            if (gridLogic[i - 1][j - 1] == 9) n++;
            if (gridLogic[i - 1][j + 1] == 9) n++;
            if (gridLogic[i + 1][j - 1] == 9) n++;
            gridLogic[i][j] = n;
        }
}

void randomFiller(int gridView[12][12], int gridLogic[12][12], int &flags) {
    for (int i = 1; i <= 10; i++) {
        for (int j = 1; j <= 10; j++) {
            gridView[i][j] = 10;
            if (rand() % 5 == 0) {
                gridLogic[i][j] = 9;
                flags++;
            }
            else {
                gridLogic[i][j] = 0;
            }
        }
    }
}

int checkWin(int gridLogic[12][12], int gridView[12][12]) {
    int counter = 0;
    for (int i = 1; i <= 10; i++)
        for (int j = 1; j <= 10; j++)
            if (gridView[i][j] == gridLogic[i][j])
                counter++;
    return counter;
}

void openSquares(int gridLogic[12][12], int gridView[12][12], int x, int y) {
    gridView[x][y] = 0;
    for (int i = -1; i <= 1; i++) {
        for (int j = -1; j <= 1; j++) {
            if (gridLogic[x + i][y + j] != 9 && gridLogic[x + i][y + j] != 0)
                gridView[x + i][y + j] = gridLogic[x + i][y + j];
        }
    }
    if (gridLogic[x - 1][y] == 0 && gridView[x - 1][y] == 10) {
        openSquares(gridLogic, gridView, x - 1, y);
    }
    if (gridLogic[x][y - 1] == 0 && gridView[x][y - 1] == 10) {
        openSquares(gridLogic, gridView, x, y - 1);
    }
    if (gridLogic[x + 1][y] == 0 && gridView[x + 1][y] == 10) {
        openSquares(gridLogic, gridView, x + 1, y);
    }
    if (gridLogic[x][y + 1] == 0 && gridView[x][y + 1] == 10) {
        openSquares(gridLogic, gridView, x, y + 1);
    }
    return;
}

int main()
{
    srand(time(0));

    sf::RenderWindow window(sf::VideoMode({ 400, 400 }), "MineSweeper!");

    bool firstClick = true;
    int w = 32;
    int gridLogic[12][12];
    int gridView[12][12];
    bool gameOver = false;
    bool win = false;
    int flags = 0;
    bool newGame = false;

    Font arial;
    arial.openFromFile("fonts\\arial\\ARIAL.TTF");
    Text text(arial);
    text.setFillColor(Color::Black);

    Texture t;
    t.loadFromFile("images\\blocks.jpg");
    Sprite s(t);

    randomFiller(gridView, gridLogic, flags);

    const int mines = flags;

    numberFiller(gridLogic);

    while (window.isOpen())
    {
        sf::Vector2i pos = Mouse::getPosition(window);
        int x = pos.x / w;
        int y = pos.y / w;

        while (const std::optional event = window.pollEvent())
        {
            if (event->is<sf::Event::Closed>())
                window.close();

            if (event->is<sf::Event::MouseButtonPressed>()) {

                if (sf::Mouse::isButtonPressed(sf::Mouse::Button::Left)) {
                    if ((gameOver || win) && (x>=8 && x<=10 && y==0)) {
                        newGame = true;
                        text.setOutlineThickness(0.f);
                        text.setFillColor(Color::Black);
                    }
                    gridView[x][y] = gridLogic[x][y];
                    if (firstClick) {
                        firstClick = false;
                        openSquares(gridLogic, gridView, x, y);
                    }
                    if (mines == 100 - checkWin(gridLogic, gridView)) 
                        win = true;
                    else if (gridView[x][y] == 9)
                        gameOver = true;
                    else if (gridView[x][y] == 0)
                        openSquares(gridLogic, gridView, x, y);
                }
                else if (sf::Mouse::isButtonPressed(sf::Mouse::Button::Right)) {
                    if (gridView[x][y] == 11) {
                        gridView[x][y] = 10;
                        flags++;
                    }
                    else if (gridView[x][y] == 10) {
                        gridView[x][y] = 11;
                        flags--;
                    }
                }
            }

        }       
        
        if (newGame) {
            firstClick = true;
            gameOver = false;
            win = false;
            flags = 0;
            newGame = false;
            randomFiller(gridView, gridLogic, flags);
            numberFiller(gridLogic);
        }

        if (gameOver){
            window.clear(Color::Black);
            for (int i = 1; i <= 10; i++)
                for (int j = 1; j <= 10; j++) {
                    gridView[i][j] = gridLogic[i][j];
                    s.setTextureRect(sf::IntRect({ gridView[i][j] * w, 0 }, { w, w }));
                    s.setPosition({ (float)i * w, (float)j * w });
                    window.draw(s);
                }
            text.setString("YOU LOOSE...    Reset?");
            text.setPosition({ 25, 0 });
            text.setFillColor(Color::White);
            text.setOutlineColor(Color::Red);
            text.setOutlineThickness(3.f);
            window.draw((text));
        }
        else if (win){
            window.clear(sf::Color::Black);
            text.setString("YOU WIN!         Reset?");
            text.setPosition({ 25, 0 });
            text.setFillColor(Color::White);
            text.setOutlineColor(Color::Blue);
            text.setOutlineThickness(3.f);
            window.draw(text);
        }
        else {
            window.clear(sf::Color::White);
            for (int i = 1; i <= 10; i++)
                for (int j = 1; j <= 10; j++) {
                    s.setTextureRect(sf::IntRect({ gridView[i][j] * w, 0 }, { w, w }));
                    s.setPosition({ (float)i * w, (float)j * w });
                    window.draw(s);
                }
            text.setString("Flags:" + std::to_string(flags));
            window.draw(text);
        }
        
        window.display();
    }
    return 0;
}