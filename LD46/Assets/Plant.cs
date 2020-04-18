using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantStuff
{
    public class Plant
    {
        enum PlantSizes
        {
            Large = 15, 
            Medium = 10, 
            Small = 5
        }

        public int RootDepth { get; set; } = 1;
        public int HP { get; set; } = 5;
        public int MaxHP = 5;

        PlantSizes PlantSize;

        public void AddRootGrowth()
        {
            this.RootDepth++;
        }

        public void AddHeath()
        {
            this.HP++;
            if(this.HP > MaxHP)
            {
                this.HP = MaxHP;
            }
        }

        public void RemoveHealth()
        {
            this.HP--;
        }
    }
}
