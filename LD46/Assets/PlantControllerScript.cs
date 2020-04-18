using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlantStuff;

public class PlantControllerScript : MonoBehaviour
{
    public Plant _Plant;
    public Planter _Planter;
    public bool AddedWaterToday = false;
    public bool HealthRemoved = false;

    public Sprite PlantSprite;

    /*  Reset Vars
     *  This occurs every 6 ticks (hours)
     */
    public void ResetVars()
    {
        this.AddedWaterToday = false;
        this.HealthRemoved = false;
    }


    /*  Assess Plant
     *  Compares the water level in the planter to the root level of the plant
     *  Removes health if not enough water, too much water, or too many roots for the planter.
     */
    public void AssessPlant()
    {
        int rootWaterFactor = _Plant.RootDepth + _Planter.WaterLevel;

        // If the roots aren't in the water, remove HP 
        if (rootWaterFactor < 16)
        {
            _Plant.RemoveHealth();
            HealthRemoved = true;
        }

        // If the roots are too wet, remove HP
        if (rootWaterFactor >= 18)
        {
            _Plant.RemoveHealth();
            HealthRemoved = true;
        }

        // If the roots are too big for the container, remove HP
        if (_Plant.RootDepth > _Planter.GetSize() + 2)
        {
            _Plant.RemoveHealth();
            HealthRemoved = true;
        }

        // If the plant hasn't lost HP. Add HP
        if (!HealthRemoved)
        {
            _Plant.AddHeath();
        }
    }

    public void AddWater()
    {
        _Planter.AddWater();
    }
}
