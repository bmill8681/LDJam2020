using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlantStuff
{


    public class Planter : MonoBehaviour
    {
        public enum PlanterSizes
        {
            XLarge = 16,
            Large = 12,
            Medium = 8,
            Small = 4
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
            int sizeOfPlanter = (int)this.PlanterSize;
               
            switch (sizeOfPlanter)
            { 
                case (int)PlanterSizes.XLarge:
                    if (this.WaterLevel < (int)PlanterSizes.Large)
                    {
                        this.WaterLevel++;
                        addedWater = true;
                    }
                    break;
                case (int)PlanterSizes.Large:
                    if (this.WaterLevel < (int)PlanterSizes.Large)
                    {
                        this.WaterLevel++;
                        addedWater = true;
                    }
                    break;
                case (int)PlanterSizes.Medium:
                    if (this.WaterLevel < (int)PlanterSizes.Medium)
                    {
                        this.WaterLevel++;
                        addedWater = true;
                    }

                    break;
                case (int)PlanterSizes.Small:
                    if (this.WaterLevel < (int)PlanterSizes.Small)
                    {
                        this.WaterLevel++;
                        addedWater = true;
                    }
                    break;
            }
            return addedWater;
        }

        public void ReduceWater()
        {
            this.WaterLevel--;
            if(WaterLevel < 0)
            {
                WaterLevel = 0;
            }
        }

        public void PrintPlanterStatus()
        {
            Debug.Log(string.Format("Plater Water Capacity: {0}, Current Water Level: {1}", this.GetSize(), this.WaterLevel));
        }
    }
}