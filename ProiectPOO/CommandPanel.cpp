#include<iostream>
#include"CommandPanel.h"
#include"RecipeCake.h"
#include"CommandTaker.h"
#include"Cake.h"

using namespace std;

CommandPanel::CommandPanel() {}

void CommandPanel::showProducts() {
	for (RecipeCake product : menu) {
		cout << "Numele produsului: " << product.getName() << endl;
		cout << "Timpul de preparare al produsului (in secunde): " << product.getTime() << endl;
	}
}

void CommandPanel::selectProduct(string name) {
	Cake product;
	int search = 0;
	for (RecipeCake recipe : menu) { //se cauta numele prajiturii in meniu
		if (recipe.getName() == name) {
			search = 1;
			commandTaker.carouselRecipe = recipe;
			product = commandTaker.takeCommand(recipe); //daca se gaseste se plaseaza comanda
			cout << "Prajitura a fost servita !" << endl;
		}
	}
	if (search == 0)
		cout << "Produsul nu exista in meniu !" << endl;
}

void CommandPanel::selectProduct(string name, int numberOfProducts) {
	Cake* products;
	int search = 0;
	for (RecipeCake recipe : menu) { //se cauta numele prajiturilor in meniu
		if (recipe.getName() == name) {
			search = 1;
			commandTaker.carouselRecipe = recipe;
			products = commandTaker.takeCommand(recipe, numberOfProducts); //daca se gasesc se plaseaza comanda
			cout << "Prajiturile au fost servite !" << endl;
		}
	}
	if (search == 0)
		cout << "Produsele nu exista in meniu !" << endl;
}

void CommandPanel::showProductsInCarousel() {
	Cake* carouselStorage;
	Cake products[12];
	carouselStorage = commandTaker.getCakesFromCarousel(); //aici se afla prajiturile din depozit
	for (int i = 0; i < 12; i++) {
		products[i] = *(carouselStorage + i);
	}
	cout << "Produsele din carusel sunt : " << endl;
	for (int i = 0; i < 12; i++) {
		if (products[i].getName() != "empty") //se cauta pozitiile ocupate de prajituri
			cout << products[i].getName() << " "; //si se afiseaza numele lor
	}
	cout << endl;
}