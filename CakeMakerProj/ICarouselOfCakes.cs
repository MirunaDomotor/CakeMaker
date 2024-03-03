using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeMakerProj
{
    public interface ICarouselOfCakes
    {
        void AddCake(Cake cake);
        Cake GetCake(string name);
        int GetCurrentCapacity();
        public uint GetMaxCapacity();
        public uint GetLowLimit();
        public Cake[] GetStorage();

    }
}
