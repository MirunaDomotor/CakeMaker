#include "CarouselOfCakes.h"

using namespace std;

CarouselOfCakes::CarouselOfCakes() {
	Cake emptyCake("empty");
	for (int i = 0; i < maxCapacity; i++) { //se creaza depozitul de prajituri, care initial este gol
		storage[i] = emptyCake;
	}
}

Cake CarouselOfCakes::getCake(string name) {
	Cake search;
	Cake emptyCake("empty");
	int currentCapacity = getCurrentCapacity();
	for (int i = 0; i < currentCapacity; i++) { //are loc cautarea prajiturii in depozit
		if (storage[i].getName() == name) {
			search = storage[i]; //daca exista se va salva in "search"
			for (int j = i; j < currentCapacity - 1; j++) //si se va sterge din depozit
				storage[j] = storage[j + 1];
			currentCapacity--; //se scade numarul de prajituri din depozit fiindca am scos una din ele
			storage[currentCapacity] = emptyCake; //ultimul loc va fi gol acum
			return search; //se returneaza prajitura gasita
		}
	}
	return emptyCake; //daca nu a fost gasita prajitura, se returneaza o prajitura "empty"
}

int CarouselOfCakes::getCurrentCapacity() {
	int contor = 0;
	for (int i = 0; i < maxCapacity; i++) {
		if (storage[i].getName() != "empty") //se contorizeaza numarul de locuri ocupate din depozitul de prajituri
			contor++;
	}
	return contor; //se returneaza numarul de prajituri aflate in depozit la momentul apelarii
}
