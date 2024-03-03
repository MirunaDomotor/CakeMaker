using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeMakerProj
{
    public class CarouselOfCakes : ICarouselOfCakes
    {
        private Cake[] Storage = new Cake[12];
        private readonly uint MaxCapacity = 12;
        private readonly uint LowLimit = 3;

        public CarouselOfCakes()
        {
            Cake emptyCake = new Cake("empty");
            // Initialize the cake storage with empty cakes
            for (int i = 0; i < MaxCapacity; i++)
            {
                Storage[i] = emptyCake;
            }
        }

        public uint GetMaxCapacity()
        {
            return MaxCapacity;
        }

        public uint GetLowLimit()
        {
            return LowLimit;
        }

        public Cake[] GetStorage()
        {
            return Storage;
        }

        public void AddCake(Cake cake)
        {
            int currentCapacity = GetCurrentCapacity();
            if (currentCapacity < MaxCapacity)
            {
                Storage[currentCapacity] = cake;
                currentCapacity++;
            }
            else
            {
                Console.WriteLine("The carousel is already full!");
            }
        }

        public Cake GetCake(string name)
        {
            Cake? searchedCake = null;
            Cake emptyCake = new Cake("empty");
            int currentCapacity = GetCurrentCapacity();

            for (int i = 0; i < currentCapacity; i++)
            {
                if (Storage[i].GetName() == name)
                {
                    searchedCake = Storage[i];

                    // Remove the found cake from the storage
                    for (int j = i; j < currentCapacity - 1; j++)
                    {
                        Storage[j] = Storage[j + 1];
                    }

                    currentCapacity--;
                    Storage[currentCapacity] = emptyCake;
                    return searchedCake;
                }
            }

            return emptyCake;
        }

        public int GetCurrentCapacity()
        {
            int count = 0;
            for (int i = 0; i < MaxCapacity; i++)
            {
                if (Storage[i].GetName() != "empty")
                {
                    count++;
                }
            }
            return count;
        }
    }
}
