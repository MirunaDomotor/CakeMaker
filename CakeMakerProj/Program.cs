using CakeMakerProj;
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int opt, nrProduse;
        string numeProdus;
        List<RecipeCake> menu = new List<RecipeCake>();

        // Cake recipes
        RecipeCake recipe1 = new RecipeCake("Cheesecake", 2);
        RecipeCake recipe2 = new RecipeCake("Minty Delight", 3);
        RecipeCake recipe3 = new RecipeCake("Lemon Fiesta", 4);
        RecipeCake recipe4 = new RecipeCake("Tiramisu", 5);

        // The recipes are being added to the menu
        menu.Add(recipe1);
        menu.Add(recipe2);
        menu.Add(recipe3);
        menu.Add(recipe4);

        CommandPanel commandPanel = new CommandPanel();
        commandPanel.SetMenu(menu);

        do
        {
            Console.WriteLine();
            Console.WriteLine("0.Exit");
            Console.WriteLine("1.Display products in the menu");
            Console.WriteLine("2.Select product");
            Console.WriteLine("3.Display products in the carousel");
            Console.Write("Enter your option: ");

            if (!int.TryParse(Console.ReadLine(), out opt))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }
            Console.WriteLine();

            switch (opt)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    commandPanel.ShowProducts();
                    break;
                case 2:
                    Console.Write("Enter the name of the product you want: ");
                    numeProdus = Console.ReadLine();
                    Console.Write("Enter the number of products you want: ");

                    if (!int.TryParse(Console.ReadLine(), out nrProduse))
                    {
                        Console.WriteLine("Invalid input for the number of products. Please enter a number.");
                        continue;
                    }
                    Console.WriteLine();

                    if (nrProduse > 1)
                        commandPanel.SelectProduct(numeProdus, nrProduse);
                    else
                        commandPanel.SelectProduct(numeProdus);
                    break;
                case 3:
                    commandPanel.ShowProductsInCarousel();
                    break;
                default:
                    Console.WriteLine("Invalid option entered!");
                    break;
            }

        } while (opt != 0);
    }
}
