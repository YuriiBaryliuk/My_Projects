#include "WorldObjects.h"

WorldObjects::WorldObjects(const WorldObjectsStruct& objectData) :
	m_texture(objectData.s_path), m_sprite(m_texture) {
	m_texture.setRepeated(objectData.s_repeater);
	m_sprite.setPosition(objectData.s_position);
	if (objectData.s_repeater)
		m_sprite.setTextureRect({ { 0, 0 }, { 1200, 256 } });
}

const Sprite& WorldObjects::getSprite() const { return m_sprite; }