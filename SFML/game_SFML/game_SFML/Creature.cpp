#include "Creature.h"
#include <iostream>

Creature::Creature(sf::Texture texture, float startPosX, float startPosY, int jumpHeight) :
	m_texture(texture), m_sprite(m_texture), m_startPosX(startPosX), m_startPosY(startPosY), m_jumpHeight(jumpHeight) {

	m_sprite.setPosition(getPosition());
	m_sprite.setOrigin(getOrigin());
}

bool Creature::getJumping() { return m_isJumping; }

void Creature::setJumping() { m_isJumping = true; }

void Creature::jump(sf::Clock& clock) {
	if (m_isJumping){
		float elapsed = clock.getElapsedTime().asSeconds();
		m_velocity = sqrt(2 * g * m_jumpHeight);
		float posY = m_startPosY - (m_velocity * elapsed - 0.5f * g * elapsed * elapsed);
		setPosition({ getPosition().x, posY });

		if (getPosition().y >= yGroundPoint) {
			m_isJumping = false;
		}
	}
}

const sf::Sprite& Creature::getSprite() const { return m_sprite; }

void Creature::draw(sf::RenderTarget& target, sf::RenderStates states) const{
	states.transform *= getTransform();
	target.draw(m_sprite, states);
}