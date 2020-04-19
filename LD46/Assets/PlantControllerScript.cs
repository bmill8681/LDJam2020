using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlantStuff;

public class PlantControllerScript : MonoBehaviour
{
    [SerializeField]
    private bool IsPlanted;

    public Plant _Plant;
    public Planter _Planter;
    public bool AddedWaterToday = false;
    public bool HealthRemoved = false;

    public GameObject PlantObject;
    public GameObject PlanterObject;
    public GameObject NewPlant;
    public List<Transform> NewPlantPosition;
    int PlantSpawnIndex = 0;

    private void Awake()
    {
        this._Planter = PlanterObject.GetComponent<Planter>();
        this.IsPlanted = false;
    }

    /*  Reset Vars
     *  This occurs every 6 ticks (hours)
     */
    public void ResetVars()
    {
        this.AddedWaterToday = false;
        this.HealthRemoved = false;
    }

    public void AttachPlant(Plant plant)
    {
        this._Plant = plant;
        this.IsPlanted = true;
        plant.GetComponent<Transform>().position = PlantObject.transform.position;
        plant.GetComponent<Transform>().SetParent(PlantObject.transform);
    }


    /*  Assess Plant
     *  Compares the water level in the planter to the root level of the plant
     *  Removes health if not enough water, too much water, or too many roots for the planter.
     */
    public void AssessPlant()
    {
        int rootWaterFactor = GetRootWaterFactor();
        
        // If the roots aren't in the water, remove HP 
        if (rootWaterFactor == -1)
        {
            _Plant.RemoveHealth();
            HealthRemoved = true;
        }

        // If the roots are too wet, remove HP
        if (rootWaterFactor == 1)
        {
            _Plant.RemoveHealth();
            HealthRemoved = true;
        }

        // If the roots are too big for the container, remove HP
        if (_Plant.RootDepth > _Planter.GetSize() + 2)
        {
            _Plant.RemoveHealth();
            HealthRemoved = true;
            Debug.Log(string.Format("Removed HP because the container is too small - Container Size: {0}, Roots: {1}", _Planter.GetSize(), _Plant.RootDepth));
        }

        // If the plant hasn't lost HP. Add HP
        if (!HealthRemoved)
        {
            _Plant.AddHeath();
        }

        _Plant.PrintPlantStatus();
        _Planter.PrintPlanterStatus();
        _Planter.ReduceWater();
    }

    private int GetRootWaterFactor()
    {
        int maxWaterLevel = _Planter.GetSize();

        if(_Planter.WaterLevel < (maxWaterLevel - _Plant.RootDepth - 2))
        {
            Debug.Log(string.Format("Planter Holds: {0}, Roots at: {1}, Water at: {2}", _Planter.GetSize(), _Planter.GetSize() - _Plant.RootDepth, _Planter.WaterLevel));
            Debug.Log(string.Format("Removed HP because the roots are't in water - Water Level: {0}, Roots: {1}, Factor: {2}", _Planter.WaterLevel, _Plant.RootDepth, maxWaterLevel - _Plant.RootDepth - 2));
            return -1;
        }

        if(_Planter.WaterLevel > maxWaterLevel - _Plant.RootDepth + 2)
        {
            Debug.Log(string.Format("Removed HP because the roots are too wet - Water Level: {0}, Roots: {1}, Factor: {2}", _Planter.WaterLevel, _Plant.RootDepth, maxWaterLevel - _Plant.RootDepth + 2));
            return 1;
        }

        return 0;
    }

    public void AddWater()
    {
        AddedWaterToday = _Planter.AddWater();
    }

    public void RemovePlant()
    {
        this._Plant = null;
    }

    public string PrintWaterLevel()
    {
        return _Planter.WaterLevel.ToString();
    }

    public void SheerPlant()
    {
        if(this._Plant != null)
        {
            if (this._Plant.Sheer())
            {
                SpawnNewPlant();
            }
        }
    }

    private void SpawnNewPlant()
    {
        GameObject newPlant = Instantiate(NewPlant) as GameObject;
        newPlant.transform.position = NewPlantPosition[PlantSpawnIndex].position;
        newPlant.transform.SetParent(NewPlantPosition[PlantSpawnIndex]);
        IncrementPlantSpawnIndex();

        // Add Animation Code Here
        newPlant.GetComponent<Plant>().DetachNewPlant();
    }

    private void IncrementPlantSpawnIndex()
    {
        PlantSpawnIndex++;
        if(PlantSpawnIndex >= 3)
        {
            PlantSpawnIndex = 0;
        }
    }
}
