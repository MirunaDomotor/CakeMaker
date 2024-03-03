using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeMakerProj
{
    public class RecipeCake
    {
        private string Name { get; set; }
        private int Time { get; set; }
        public RecipeCake()
        {
            this.Name = "";
            this.Time = 0;
        }
        public RecipeCake(string name, int time)
        {
            this.Name = name;
            if (time >= 0)
            {
                this.Time = time; ; //the preparation time of the cake recipe
            }
            else
            {
                this.Time = 0;
            }
        }
        public void SetName(string name)
        {
            this.Name = name;
        }
        public string GetName()
        {
            return Name;
        }
        public void SetTime(int time)
        {
            if (time >= 0)
            {
                this.Time = time;
            }
            else
            {
                Console.WriteLine("Error: Preparation time cannot be negative. It will be set to 0.");
                this.Time = 0;
            }
        }
        public int GetTime()
        {
            return Time;
        }
    }
}
