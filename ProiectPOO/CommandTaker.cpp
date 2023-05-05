#include "CommandTaker.h"
#include<iostream>

using namespace std;

CommandTaker::CommandTaker() {}

Cake CommandTaker::takeCommand(RecipeCake recipe) {
	if (checkCarouselOfCakes() == 0) { //daca capacitatea din depozit este mica
		refillCarousel(); //se reumple depozitul
	}
	Cake carouselCake;
	carouselCake = carousel.getCake(recipe.getName());
	if (carouselCake.getName() != "empty")
		return carouselCake; //daca prajitura cautata se afla in depozit, se va lua de acolo
	else
		return cakeMaker.takeCommand(recipe); //daca prajitura cautata nu se afla in depozit, va fi creata
}

Cake* CommandTaker::takeCommand(RecipeCake recipe, int nrOfCakes) {
	Cake cakes[12]; //s-a creat un vector in care vor fi puse prajiturile comandate
	Cake emptyCake("empty");
	for (int i = 0; i < carousel.maxCapacity; i++) {
		cakes[i] = emptyCake; //unde nu vor fi puse prajituri efective, vor fi prajituri "empty" pentru a facilita returnarea
	}
	for (int i = 0; i < nrOfCakes; i++) {
		if (checkCarouselOfCakes() == 0){ //daca capacitatea din depozit este mica
			refillCarousel(); //se reumple depozitul
		}
		Cake carouselCake;
		carouselCake = carousel.getCake(recipe.getName());
		if (carouselCake.getName() != "empty")
			cakes[i] = carouselCake; //daca prajitura cautata se afla in depozit, se va lua de acolo
		else
			cakes[i] = cakeMaker.takeCommand(recipe); //daca prajitura cautata nu se afla in depozit, va fi creata
	}
	return cakes; //se returneaza prajiturile comandate
}


Cake* CommandTaker::getCakesFromCarousel() {
	return carousel.storage; //se returneaza prajiturile din depozit
}

bool CommandTaker::checkCarouselOfCakes() {
	int k = 0;
	for (int i = 0; i < carousel.maxCapacity; i++){
		if (carousel.storage[i].getName() != "empty") //se numara prajiturile din depozit
			k++;
	}
	if (k <= carousel.lowLimit - 1) //se verifica daca capacitatea din depozit este mica sau nu
		return 0; //daca este o capacitate mica, se returneaza 0
	else
		return 1; //daca capacitatea este peste limita inferiora, se returneaza 1
}

void CommandTaker::refillCarousel() {
	cout << "Se umple caruselul !" << endl;
	int k = 0;
	k = carousel.getCurrentCapacity(); //se calculeaza capacitatea curenta din depozit
	if (k <= carousel.lowLimit - 1) { //daca capacitatea este sub limita inferioara
		int opt;
		for (int i = k; i < carousel.maxCapacity; i++) { //se adauga prajituri pana depozitul ajunge la capacitatea maxima
			opt = rand() % 4;
			RecipeCake recipe1("Cheesecake", 2);
			RecipeCake recipe2("Negresa", 3);
			RecipeCake recipe3("Dobos", 4);
			RecipeCake recipe4("Tiramisu", 5);
			switch (opt) { //se adauga prajituri in mod aleatoriu in depozit
			case 0: carousel.storage[i] = cakeMaker.takeCommand(recipe1);
				break;
			case 1: carousel.storage[i] = cakeMaker.takeCommand(recipe2);
				break;
			case 2: carousel.storage[i] = cakeMaker.takeCommand(recipe3);
				break;
			case 3: carousel.storage[i] = cakeMaker.takeCommand(recipe4);
				break;
			}
		}
	}
}
