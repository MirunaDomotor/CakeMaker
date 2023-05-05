#include<iostream>
#include<Windows.h>
#include "CakeMaker.h"

using namespace std;

CakeMaker::CakeMaker() {}

Cake CakeMaker::takeCommand(RecipeCake recipe) {
	string nameCake;
	cout << endl << "Desertul " << recipe.getName() << " va fi gata in " << recipe.getTime() << " secunde" << endl;
	Sleep(recipe.getTime() * 1000); //se asteapta numarul de secunde corespunzator timpului retetei
	cout << "A fost pregatit desertul " << recipe.getName() << "!" << endl;
	nameCake = recipe.getName();
	Cake orderedCake(nameCake); //are loc crearea prajiturii
	return orderedCake; //se returneaza prajitura
}