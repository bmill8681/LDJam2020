using PlantTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheerPlantTool : MonoBehaviour
{
    PlantControllerScript PC;

    private void Start()
    {
        this.PC = GetComponent<PlantControllerScript>();
    }

    private void OnMouseDown()
    {
        if (ToolManagerScript.Instance.CheckActiveTool(ToolManagerScript.Tools.Sheers))
        {
            PC.SheerPlant();
        }
    }
}
