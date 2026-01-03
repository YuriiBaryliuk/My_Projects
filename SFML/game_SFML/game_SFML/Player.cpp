#include "Player.h"

Player::Player(std::string name, int level) : m_name(name), m_level(level) {}

void Player::printData (){
	std::cout << "Player's name: " << m_name << " and level: " << m_level << std::endl;
}