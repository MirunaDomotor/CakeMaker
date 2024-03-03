using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeMakerProj
{
    public class CarouselOfCakesStub : ICarouselOfCakes
    {
        private Cake[] cakes = new Cake[12];
        private readonly uint MaxCapacity = 12;
        private readonly uint LowLimit = 3;

        public CarouselOfCakesStub(Cake[] cakes)
        {
            this.cakes = cakes;
        }

        public void AddCake(Cake cake)
        {
            // Implementare după necesități, nu este necesar să facem nimic pentru stub
        }

        public Cake GetCake(string name)
        {
            // Implementare după necesități, nu este necesar să facem nimic pentru stub
            return cakes.FirstOrDefault(c => c.GetName() == name);
        }

        public int GetCurrentCapacity()
        {
            int count = 0;
            for (int i = 0; i < MaxCapacity; i++)
            {
                if (cakes[i].GetName() != "empty")
                {
                    count++;
                }
            }
            return count;
        }

        uint ICarouselOfCakes.GetLowLimit()
        {
            return LowLimit;
        }

        uint ICarouselOfCakes.GetMaxCapacity()
        {
            return MaxCapacity;
        }

        Cake[] ICarouselOfCakes.GetStorage()
        {
            return cakes;
        }
    }
}
