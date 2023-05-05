#pragma once

using namespace std;

class RecipeCake { 
private:
	string name;
	int time = 0;
public:
	RecipeCake();
	RecipeCake(string name, int time);
	string getName();
	int getTime();
};

