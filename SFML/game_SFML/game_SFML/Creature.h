#pragma once
#include <SFML/System/Clock.hpp>
#include <SFML/System/Time.hpp>
#include <SFML/Graphics.hpp>
#include <cstdlib>
#include <math.h>

const int yGroundPoint = 344;
const float g = 500.f;

class Creature : public sf::Transformable, public sf::Drawable {
private:
	float m_velocity = 0;
	bool m_isJumping = false;
protected:
	sf::Texture m_texture;
	sf::Sprite m_sprite;
	int m_jumpHeight;
	float m_startPosY;
	float m_startPosX;
public:
	Creature(sf::Texture texture, float startPosX = 0, float startPosY = 0, int jumpHeight = 50);
	bool getJumping();
	void setJumping();
	void jump(sf::Clock &clock);
	const sf::Sprite& getSprite() const;
private:
	void draw(sf::RenderTarget& target, sf::RenderStates states) const override;
};