// In this game you need to find a number from 0 to 999
// "Digit you found" means that the digit here is in the number
// "Equivalent digit" means that the digit here is in the and on correct position

#include <iostream>
#include <random>
#include <array>
using namespace std;

int posFinder(array<int, 3>& computerNumber, array<int, 3>& userNumber) {
	int counter = 0;
	for (int index = 0; index < 3; ++index)
		if (computerNumber[index] == userNumber[index])
			++counter;

	return counter;
}

int trueFinder(array<int, 3>& computerNumber, array<int, 3>& userNumber) {
	int counter = 0;
	for (int i = 0; i < 3; ++i) {
		for (int j = 0; j < 3; ++j) {
			if (userNumber[j] == computerNumber[i]) {
				++counter;
				break;
			}
		}
	}

	return counter;
}

array<int, 3> getArray(int fullNum) {
	return { fullNum / 100, (fullNum % 100) / 10, fullNum % 10 };
}

mt19937 randomInt() {
	random_device rd;
	seed_seq ss{ rd() };
	mt19937 mt(ss);
	return mt;
}

int main() {

	mt19937 randomInteger = randomInt();
	uniform_int_distribution ds{ 0, 999 };

	array<int, 3> computerNumber = getArray(ds(randomInteger));


	while (true) {
		cout << "Input a number in range 0 to 999:\n";
		int userInput;
		cin >> userInput;
		cout << "Your number is: " << userInput << endl;

		array<int, 3> userNumber = getArray(userInput);


		if (trueFinder(computerNumber, userNumber) == 3 && posFinder(computerNumber, userNumber) == 3)
			break;
		else
			cout << "\nDigits you found: " << trueFinder(computerNumber, userNumber)
			<< "\nEquivalent digits: " << posFinder(computerNumber, userNumber) << endl;
	}

	cout << "\nYou did it!";

	return 0;
}