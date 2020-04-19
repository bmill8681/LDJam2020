using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlantCounterScript : MonoBehaviour
{
    public TextMeshProUGUI TMP_NumPlants;
    public void UpdatePlantCounter(int count)
    {
        TMP_NumPlants.SetText(count.ToString());
    }
}
