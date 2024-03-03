using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeMakerProj
{
    public class CommandPanel
    {
        private List<RecipeCake> Menu;
        private ICommandTaker CommandTaker;

        public CommandPanel()
        {
            this.Menu=new List<RecipeCake>();
            this.CommandTaker=new CommandTaker();
        }

        public void SetMenu(List<RecipeCake> menu)
        {
            this.Menu = menu; 
        }

        public void SetCommandTaker(ICommandTaker commandTaker)
        {
            this.CommandTaker = commandTaker;
        }

        public void ShowProducts()
        {
            foreach (RecipeCake product in Menu)
            {
                Console.WriteLine("Product Name: " + product.GetName());
                Console.WriteLine("Preparation Time (in seconds): " + product.GetTime());
            }
        }

        public void SelectProduct(string name)
        {
            Cake product;
            int search = 0;
            foreach (RecipeCake recipe in Menu)
            {
                if (recipe.GetName() == name)
                {
                    search = 1;
                    CommandTaker.SetRecipeCake(recipe);
                    product = CommandTaker.TakeCommand(recipe);
                    Console.WriteLine("The cake has been served!");
                }
            }

            if (search == 0)
                Console.WriteLine("The product does not exist in the menu!");
        }

        public void SelectProduct(string name, int numberOfProducts)
        {
            Cake[] products;
            int search = 0;
            foreach (RecipeCake recipe in Menu)
            {
                if (recipe.GetName() == name)
                {
                    search = 1;
                    CommandTaker.SetRecipeCake(recipe);
                    products = CommandTaker.TakeCommand(recipe, numberOfProducts);
                    Console.WriteLine("The cakes have been served!");
                }
            }

            if (search == 0)
                Console.WriteLine("The products do not exist in the menu!");
        }

        public void ShowProductsInCarousel()
        {
            Cake[] carouselStorage;
            Cake[] products = new Cake[12];
            carouselStorage = CommandTaker.GetCakesFromCarousel();

            for (int i = 0; i < 12; i++)
            {
                products[i] = carouselStorage[i];
            }

            Console.WriteLine("The products in the carousel are:");
            for (int i = 0; i < 12; i++)
            {
                if (products[i].GetName() != "empty")
                    Console.Write(products[i].GetName() + " | ");
            }
            Console.WriteLine();
        }
    }
}
