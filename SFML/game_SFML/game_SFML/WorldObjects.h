#pragma once
#include <SFML/Graphics/Texture.hpp>
#include <SFML/Graphics/Sprite.hpp>

using namespace sf;

struct WorldObjectsStruct {
	std::string s_path;
	Vector2f s_position;
	bool s_repeater;
};

class WorldObjects {
private:
	Texture m_texture;
	Sprite m_sprite;
public:
	WorldObjects(const WorldObjectsStruct& objectData);
	const Sprite& getSprite() const;
};