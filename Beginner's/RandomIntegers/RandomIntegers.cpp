#include <iostream>
#include <random>
int main() {

	std::random_device rd{};
	std::seed_seq ss{ rd() };
	std::mt19937 mt{ ss };

	std::uniform_int_distribution<> distr{ 1, 6 };

	std::cout << distr(mt);

	return 0;
}