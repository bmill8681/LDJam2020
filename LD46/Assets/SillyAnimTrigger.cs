using PlantStuff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SillyAnimTrigger : MonoBehaviour
{
    public void TriggerIt(int index)
    {
        GetComponentInParent<PlantControllerScript>().TriggerPlantBabyDetacher(index);
    }
}
