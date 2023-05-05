#pragma once
#include"Cake.h"
#include"RecipeCake.h"

class CakeMaker {
public:
	CakeMaker();
	Cake takeCommand(RecipeCake recipe);
};

