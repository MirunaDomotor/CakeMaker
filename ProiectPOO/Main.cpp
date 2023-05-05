#include<iostream>
#include"CommandPanel.h"
#pragma warning(disable : 4996)

using namespace std;

int main() {
	int opt, nrProduse;
	string numeProdus;
	list<RecipeCake> menu; //se creeaza meniul
	//retetele de prajituri
	RecipeCake recipe1("Cheesecake", 2);
	RecipeCake recipe2("Negresa", 3);
	RecipeCake recipe3("Dobos", 4);
	RecipeCake recipe4("Tiramisu", 5);
	//se adauga retetele in meniu
	menu.push_back(recipe1);
	menu.push_back(recipe2);
	menu.push_back(recipe3);
	menu.push_back(recipe4);
	CommandPanel commandPanel;
	commandPanel.menu = menu;
	do {
		cout << "0.Iesire" << endl;
		cout << "1.Afisare produse din meniu" << endl;
		cout << "2.Selectare produs" << endl;
		cout << "3.Afisare produse din carusel" << endl;
		cout << "Introduceti optiunea dumnevoastra : ";
		cin >> opt;
		switch (opt) {
		case 0:exit(0);
			break;
		case 1: commandPanel.showProducts();
			break;
		case 2:
			cout << "Introduceti numele produsului pe care il doriti : ";
			cin >> numeProdus;
			cout << "Introduceti numarul de produse pe care il doriti : ";
			cin >> nrProduse;
			if (nrProduse > 1)
				commandPanel.selectProduct(numeProdus, nrProduse);
			else
				commandPanel.selectProduct(numeProdus);
			break;
		case 3: commandPanel.showProductsInCarousel();
			break;
		default: cout << "Optiunea introdusa este incorecta !" << endl;
			break;
		}
	} while (opt != 0);
	return 0;
}