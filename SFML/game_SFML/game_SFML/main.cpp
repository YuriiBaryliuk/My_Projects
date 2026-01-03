#include <SFML\Graphics.hpp>
//#include "WorldObjects.h"
//#include "Creature.h"
//#include <math.h>
//#include <fstream>
//#include <sstream>
//#include <cstdlib>
#include <iostream>

using namespace sf;

enum Directions {
	LEFT,
	RIGHT,
	UP,
	DOWN
};

void getDirection(Directions direction) {
	switch (direction) {
	case Directions::LEFT:
		std::cout << "LEFT" << std::endl;
		break;
	case Directions::RIGHT:
		std::cout << "RIGHT" << std::endl;
		break;
	case Directions::UP:
		std::cout << "UP" << std::endl;
		break;
	case Directions::DOWN:
		std::cout << "DOWN" << std::endl;
		break;
	default:
		std::cout << "NO" << std::endl;
	}
}

int main() {

	// Gravity
	float gravity = 0.01f;
	float posY = 0.f;
	sf::Clock clck;
	bool falling = true;
	bool collisionOn = false;

	float oldPositionBlackX = 0.f;
	float oldPositionBlackY = 0.f;
	float newPositionBlackX = 0.f;
	float newPositionBlackY = 0.f;

	RenderWindow window(VideoMode({ 1200, 600 }), "SFML Works!");

	RectangleShape ground({ 1200.f, 600.f });
	ground.setPosition({ 0.f, 500.f });
	ground.setFillColor(Color::Green);

	RectangleShape black({ 80.f, 50.f });
	black.setPosition({0.f, 0.f });
	black.setFillColor(Color::Black);

	RectangleShape red({ 80.f, 50.f });
	red.setPosition({ 200.f, 450.f });
	red.setFillColor(Color::Red);

	
	
	while (window.isOpen()) {
		while (const std::optional event = window.pollEvent()) {
			if (event->is<sf::Event::Closed>())
				window.close();
		}

		if (Keyboard::isKeyPressed(Keyboard::Key::D)) {
			//oldPositionBlackX = black.getPosition().x;
			//oldPositionBlackY = black.getPosition().y;
			black.move({ 0.5f, 0.f });
			//newPositionBlackX = black.getPosition().x;
			//newPositionBlackY = black.getPosition().y;
			if (black.getGlobalBounds().findIntersection(red.getGlobalBounds()).has_value()) {
				black.move({ -0.5f, 0.f });
			}
				
		}
		if (Keyboard::isKeyPressed(Keyboard::Key::A)) {
			//oldPositionBlackX = black.getPosition().x;
			//oldPositionBlackY = black.getPosition().y;
			black.move({ -0.5f, 0.f });
			//newPositionBlackX = black.getPosition().x;
			//newPositionBlackY = black.getPosition().y;
			if (black.getGlobalBounds().findIntersection(red.getGlobalBounds()).has_value()) {
				black.move({ 0.5f, 0.f });
			}
		}


		if (falling) {
			float clockInMilliSeconds = clck.getElapsedTime().asMilliseconds();
			posY = 0.5f * gravity * pow(clockInMilliSeconds, 2);
			//oldPositionBlackX = black.getPosition().x;
			//oldPositionBlackY = black.getPosition().y;
			black.setPosition({ black.getPosition().x, posY });
			//newPositionBlackX = black.getPosition().x;
			//newPositionBlackY = black.getPosition().y;
		}

		oldPositionBlackX = black.getPosition().x;
		oldPositionBlackY = black.getPosition().y;
		newPositionBlackX = black.getPosition().x;
		newPositionBlackY = black.getPosition().y;
		if(oldPositionBlackY - newPositionBlackY < 0)
			getDirection(Directions::DOWN);
		if (oldPositionBlackY - newPositionBlackY > 0)
			getDirection(Directions::UP);
		if(oldPositionBlackX - newPositionBlackX < 0)
			getDirection(Directions::RIGHT);
		if (oldPositionBlackX - newPositionBlackX > 0)
			getDirection(Directions::LEFT);
		if (oldPositionBlackX - newPositionBlackX == 0 && oldPositionBlackY - newPositionBlackY == 0)
			//std::cout << "CENTER" << std::endl;

			{
			}

		if (black.getGlobalBounds().findIntersection(ground.getGlobalBounds()).has_value()) {
			falling = false;
			black.setPosition({ black.getPosition().x, ground.getPosition().y - black.getSize().y });
		}

		/*if(oldPositionBlackX - newPositionBlackX < 0) {
			std::cout << "Old\n" << oldPositionBlackX << std::endl;
			std::cout << "New\n" << newPositionBlackX << std::endl;
		}*/

		window.clear(Color::White);
		window.draw(ground);
		window.draw(black);
		window.draw(red);
		window.display();
	}

	return 0;
}