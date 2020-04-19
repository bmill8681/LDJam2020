using PlantStuff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBabyDetacher : MonoBehaviour
{
    private Dictionary<int, Plant> SpawnLocation = new Dictionary<int, Plant>();
    public void AddSpawnerToList(int location, Plant baby)
    {
        if (!SpawnLocation.ContainsKey(location))
        {
            SpawnLocation.Add(location, baby);
        }
    }

    public void RemoveSpawnerFromList(int location)
    {
        Debug.Log("Detaching # " + location);
        Plant baby = SpawnLocation[location];
        baby.DetachNewPlant();
        SpawnLocation.Remove(location);
    }
}
