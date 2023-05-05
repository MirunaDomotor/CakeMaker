#pragma once
#include"Cake.h"

using namespace std;

class CarouselOfCakes {
private:
	Cake storage[12];
	unsigned int maxCapacity = 12;
	unsigned int lowLimit = 3;
public:
	CarouselOfCakes();
	Cake getCake(string name);
	int getCurrentCapacity();
	friend class CommandTaker;
};

