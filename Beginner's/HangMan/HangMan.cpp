// write user input check for more symbols
#include <iostream>
#include <random>
#include <array>
#include <string>
using namespace std;

int randomize() {
	random_device rd{};
	seed_seq ss{ rd() };
	mt19937 mt{ ss };
	uniform_int_distribution UID{ 0, 39 };
	return UID(mt);
}

void answerCheck(string guessWord, char userInput, string& templateWord, int& counter) {

	for (int i = 0; i < guessWord.size(); i++) {
		if (guessWord[i] == userInput && templateWord[i * 2] != userInput) {
			cout << "You have found letter " << userInput << " on position " << i + 1 << "\n";
			counter--;
			templateWord[i * 2] = userInput;
		}
	}
}

bool inputCheck(char& ch) {
	int numChar = static_cast<int>(ch);
	if (numChar < 65 || (numChar > 90 && numChar < 97) || numChar > 122) {
		cout << "\nThe letter is incorrect\n";
		return false;
	}
	else if (numChar > 60) {
		ch = static_cast<char>(numChar - 32);
		return true;
	}
}

int main() {

	array<string, 40> words
	{ "CAR", "HAPPY", "MOON", "CLOUD", "RIVER", "STONE", "LIGHT", "CHAIR", "APPLE", "BRIDGE",
	"FIRE", "WATER", "HOUSE", "GHOST", "PLANE", "MUSIC", "STORM", "DREAM", "CLOCK", "DOOR",
	"TRAIN", "GLASS", "SMILE", "LEAF", "WHEEL", "PENCIL", "CANDLE", "SWORD", "BREAD", "FROG",
	"TRUCK", "HORSE", "MAPLE", "SHELL", "CROWN", "SHADOW", "BERRY", "LUNCH", "TIGER", "NIGHT" };

	string guessWord = words.at(randomize());
	//cout << "Guess word: " << guessWord << "\n";

	int temp = guessWord.size();
	string currentWord;
	while (temp--) {
		currentWord.append("_ ");
	}

	int counter = guessWord.size();
	int lives = 6;

	while (counter && lives) {
		cout << "\nCurrent state: " << currentWord << "\n";
		cout << "Your lives: " << lives << "\n";
		cout << "\nInput one letter: ";
		char userInput;
		cin >> userInput;

		if (inputCheck(userInput)) {
			cout << "\nYour letter: " << userInput << "\n";
			int tempCounter = counter;
			size_t found = currentWord.find(userInput);
			if (found != std::string::npos) {
				cout << "\nThis letter already exists\n";
				continue;
			}

			answerCheck(guessWord, userInput, currentWord, counter);

			if (tempCounter == counter) {
				cout << "\nThere isn't such letter in that word\n";
				lives--;
			}
		}
	}

	if (counter == 0) {
		cout << "\nYou win\nThe word is: " << guessWord;
	}
	else if (lives == 0) {
		cout << "\nYou loose\nThe word is: " << guessWord;
	}

	return 0;
}