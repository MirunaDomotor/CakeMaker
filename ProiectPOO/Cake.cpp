#include "Cake.h"

Cake::Cake() {}

Cake::Cake(string name) {
	this->name = name; //numele prajiturii
}

string Cake:: getName() {
	return name;
}