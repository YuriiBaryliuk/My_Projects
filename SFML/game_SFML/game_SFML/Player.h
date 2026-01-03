#pragma once
#include <iostream>

class Player {
private:
	std::string m_name = "";
	int m_level = 1;
public:
	Player(std::string name, int level = 1);
	void printData();
};
