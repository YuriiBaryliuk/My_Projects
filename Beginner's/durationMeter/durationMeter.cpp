// Program counts an execution time of "experimental program in microseconds (10^(-6) of a second)"

#include <iostream>
#include <chrono>

int main() {

	auto start = std::chrono::high_resolution_clock::now();

//--------<Experimental program to count time for>--------
	for (int i = 0; i < 10; ++i)
		std::cout << i << " ";
//-------------------<End>-------------------
	auto stop = std::chrono::high_resolution_clock::now();
	auto duration = std::chrono::duration_cast<std::chrono::microseconds>(stop - start);
	std::cout << "\nDuration: " << duration.count() << " microseconds";

	return 0;
}