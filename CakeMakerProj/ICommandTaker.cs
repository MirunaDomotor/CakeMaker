using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeMakerProj
{
    public interface ICommandTaker
    {
        Cake TakeCommand(RecipeCake recipe);
        Cake[] TakeCommand(RecipeCake recipe, int nrOfCakes);
        public void SetRecipeCake(RecipeCake recipeCake);
        public RecipeCake GetRecipeCake();
        public void SetCakeMaker(CakeMaker cakeMaker);
        public CakeMaker GetCakeMaker();
        public void SetCarouselOfCakes(ICarouselOfCakes carouselOfCakes);
        public ICarouselOfCakes GetCarouselOfCakes();
        public Cake[] GetCakesFromCarousel();
        public bool CheckCarouselOfCakes();
        public void RefillCarousel();
    }
}
