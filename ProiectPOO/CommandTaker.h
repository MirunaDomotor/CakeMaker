#pragma once
#include"CakeMaker.h"
#include"CarouselOfCakes.h"

class CommandTaker {
private:
	RecipeCake carouselRecipe;
	CakeMaker cakeMaker;
	CarouselOfCakes carousel;
public:
	CommandTaker();
	Cake takeCommand(RecipeCake recipe);
	Cake* takeCommand(RecipeCake recipe, int nrOfCakes);
	Cake* getCakesFromCarousel();
	bool checkCarouselOfCakes();
	void refillCarousel();
	friend class CommandPanel;
};

