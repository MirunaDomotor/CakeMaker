using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeMakerProj
{
    public class Cake
    {
        private string Name;
        public Cake()
        {
            this.Name = "";
        }
        public Cake(string name)
        {
            this.Name = name;
        }
        public void SetName(string name)
        {
            this.Name = name;
        }
        public string GetName()
        {
            return Name;
        }
    }
}
