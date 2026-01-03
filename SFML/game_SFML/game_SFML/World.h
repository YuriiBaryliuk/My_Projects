#pragma once
#include "Main_SFML.h"

struct BaseFigureStyle
{
	Color s_color;
	RectangleShape s_shape;
	Vector2f s_position;
};

class World {
private:
	BaseFigureStyle m_sky, m_ground;
	Texture ground{};
public:
	World();
	void createMap();
};