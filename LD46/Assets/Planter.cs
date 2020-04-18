using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlantStuff
{


    public class Planter : MonoBehaviour
    {
        public enum PlanterSizes
        {
            Large = 15,
            Medium = 10,
            Small = 5
        }

        public PlanterSizes PlanterSize;
        public int WaterLevel;

        private void Awake()
        {
            this.WaterLevel = 0;
        }

        public int GetSize()
        {
            return (int)this.PlanterSize;
        }

        /* Adding water into the planter. 
        *   Returns true if successful. Returns false if the water overflowed. 
        */
        public bool AddWater()
        {
            bool addedWater = false;
            switch (this.PlanterSize)
            {
                case PlanterSizes.Large:
                    if (this.WaterLevel < (int)PlanterSizes.Large)
                    {
                        this.WaterLevel++;
                        addedWater = true;
                    }
                    break;
                case PlanterSizes.Medium:
                    if (this.WaterLevel < (int)PlanterSizes.Medium)
                    {
                        this.WaterLevel++;
                        addedWater = true;
                    }

                    break;
                case PlanterSizes.Small:
                    if (this.WaterLevel < (int)PlanterSizes.Small)
                    {
                        this.WaterLevel++;
                        addedWater = true;
                    }
                    break;
            }
            return addedWater;
        }
    }
}