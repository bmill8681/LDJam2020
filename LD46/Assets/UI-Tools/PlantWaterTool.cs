using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlantTools;

public class PlantWaterTool : MonoBehaviour
{
    PlantControllerScript PC;

    private void Start()
    {
        this.PC = GetComponent<PlantControllerScript>();
    }

    private void OnMouseDown()
    {
        if (ToolManagerScript.Instance.CheckActiveTool(ToolManagerScript.Tools.WateringCan))
        {
            PC.AddWater();
        }
    }
}
