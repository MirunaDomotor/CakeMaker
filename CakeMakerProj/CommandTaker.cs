using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeMakerProj
{
    public class CommandTaker : ICommandTaker
    {
        private RecipeCake CarouselRecipe;
        private CakeMaker CakeMaker;
        private ICarouselOfCakes Carousel;
        public CommandTaker() 
        {
            this.CarouselRecipe = new RecipeCake();
            this.CakeMaker = new CakeMaker();
            this.Carousel = new CarouselOfCakes();
        }
        public void SetRecipeCake(RecipeCake recipeCake)
        {
            this.CarouselRecipe = recipeCake;
        }

        public RecipeCake GetRecipeCake()
        {
            return CarouselRecipe;
        }

        public void SetCakeMaker(CakeMaker cakeMaker)
        {
            this.CakeMaker = cakeMaker;
        }

        public CakeMaker GetCakeMaker()
        {
            return CakeMaker;
        }
        public void SetCarouselOfCakes(ICarouselOfCakes carouselOfCakes)
        {
            this.Carousel = carouselOfCakes;
        }

        public ICarouselOfCakes GetCarouselOfCakes()
        {
            return Carousel;
        }

        public Cake TakeCommand(RecipeCake recipe)
        {
            if (CheckCarouselOfCakes() == false)
            {
                RefillCarousel();
            }

            Cake carouselCake = Carousel.GetCake(recipe.GetName());
            if (carouselCake.GetName() != "empty")
            {
                return carouselCake;
            }
            else
            {
                return CakeMaker.TakeCommand(recipe);
            }
        }

        public Cake[] TakeCommand(RecipeCake recipe, int nrOfCakes)
        {
            Cake[] cakes = new Cake[12];
            Cake emptyCake = new Cake("empty");

            for (int i = 0; i < Carousel.GetMaxCapacity(); i++)
            {
                cakes[i] = emptyCake;
            }

            for (int i = 0; i < nrOfCakes; i++)
            {
                if (CheckCarouselOfCakes() == false)
                {
                    RefillCarousel();
                }

                Cake carouselCake = Carousel.GetCake(recipe.GetName());
                cakes[i] = carouselCake.GetName() != "empty" ? carouselCake : CakeMaker.TakeCommand(recipe);
            }

            return cakes;
        }

        public Cake[] GetCakesFromCarousel()
        {
            return Carousel.GetStorage();
        }

        public bool CheckCarouselOfCakes()
        {
            int k = 0;
            for (int i = 0; i < Carousel.GetMaxCapacity(); i++)
            {
                if (Carousel.GetStorage()[i].GetName() != "empty")
                {
                    k++;
                }
            }

            return k <= Carousel.GetLowLimit() - 1 ? false : true;
        }

        public void RefillCarousel()
        {
            Console.WriteLine("Refilling the carousel!");
            int k = Carousel.GetCurrentCapacity();

            if (k <= Carousel.GetLowLimit() - 1)
            {
                int opt;
                for (int i = k; i < Carousel.GetMaxCapacity(); i++)
                {
                    opt = new Random().Next(4);
                    RecipeCake recipe1 = new RecipeCake("Cheesecake", 2);
                    RecipeCake recipe2 = new RecipeCake("Minty Delight", 3);
                    RecipeCake recipe3 = new RecipeCake("Lemon Fiesta", 4);
                    RecipeCake recipe4 = new RecipeCake("Tiramisu", 5);

                    switch (opt)
                    {
                        case 0:
                            Carousel.GetStorage()[i] = CakeMaker.TakeCommand(recipe1);
                            break;
                        case 1:
                            Carousel.GetStorage()[i] = CakeMaker.TakeCommand(recipe2);
                            break;
                        case 2:
                            Carousel.GetStorage()[i] = CakeMaker.TakeCommand(recipe3);
                            break;
                        case 3:
                            Carousel.GetStorage()[i] = CakeMaker.TakeCommand(recipe4);
                            break;
                    }
                }
            }
        }
    }
}
