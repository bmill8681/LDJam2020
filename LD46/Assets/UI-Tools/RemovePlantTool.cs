using PlantTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePlantTool : MonoBehaviour
{
    PlantControllerScript PC;

    private void Start()
    {
        this.PC = GetComponent<PlantControllerScript>();
    }

    private void OnMouseDown()
    {
        if (ToolManagerScript.Instance.CheckActiveTool(ToolManagerScript.Tools.Shovel) && !GameManagerScript.Instance.ToolsDisabled)
        {
            if(PC._Plant != null)
            {
                PC._Plant.RemoveFromPlanter();
                PC.RemovePlant();
                AudioManagerScript.Instance.PlayCharacterSFX("ShovelSFX", "CharacterSFXSource");
            }
        }
    }
}
