using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeMakerProj
{
    public class CakeMaker
    {
        private IThread thread;

        public CakeMaker(IThread thread)
        {
            this.thread = thread;
        }

        public CakeMaker() { }
        public Cake TakeCommand(RecipeCake recipe)
        {
            string nameCake;
            Console.WriteLine($"The dessert {recipe.GetName()} will be ready in {recipe.GetTime()} seconds!");
            Thread.Sleep(recipe.GetTime() * 1000); // wait the number of seconds corresponding to the time of the recipe
            Console.WriteLine($"The dessert {recipe.GetName()} is ready!");
            Console.WriteLine();
            nameCake = recipe.GetName();
            Cake orderedCake = new Cake(nameCake); // the cake gets created
            return orderedCake; // the cake is being returned
        }

        public Cake TakeCommand2(RecipeCake recipe)
        {
            string nameCake;
            Console.WriteLine($"The dessert {recipe.GetName()} will be ready in {recipe.GetTime()} seconds!");
            thread.Sleep(recipe.GetTime() * 1000); // wait the number of seconds corresponding to the time of the recipe
            Console.WriteLine($"The dessert {recipe.GetName()} is ready!");
            Console.WriteLine();
            nameCake = recipe.GetName();
            Cake orderedCake = new Cake(nameCake); // the cake gets created
            return orderedCake; // the cake is being returned
        }
    }
}
