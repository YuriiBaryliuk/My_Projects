#include "World.h"

// will be reading from level file
Color SKY_COLOR(sf::Color::Blue);
Color GROUND_COLOR(sf::Color::Green);
RectangleShape GROUND_SHAPE({ WINDOW_WIDTH, WINDOW_HEIGHT * 0.25 });
Vector2f GROUND_POSITION({ 0.f, static_cast<float>(WINDOW_HEIGHT - GROUND_SHAPE.getSize().y) });

World::World() {
	m_sky.s_color = SKY_COLOR;
	m_ground.s_color = GROUND_COLOR;
	m_ground.s_shape = GROUND_SHAPE;
	m_ground.s_position = GROUND_POSITION;

	/*RectangleShape ground(m_ground.s_shape);
	ground.setFillColor(m_ground.s_color);
	ground.setPosition(m_ground.s_position);*/

	ground("D:/Programs/Visual Studio/VS_Projrcts/game_SFML/Resources/images.jfif");
}

void World::createMap() {

}