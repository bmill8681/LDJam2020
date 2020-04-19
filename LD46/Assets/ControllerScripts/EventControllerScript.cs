using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlantStuff;

public class EventControllerScript : MonoBehaviour
{
    public static EventControllerScript Instance { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public event Action onPutPlantInPlanter;
    public void PutPlantInPlanter(GameObject plant, PlantControllerScript planterController)
    {
        if(onPutPlantInPlanter != null)
        {
            onPutPlantInPlanter();
        }
    }
}
