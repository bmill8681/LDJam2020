using PlantTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectPlanterTool : MonoBehaviour
{
    PlantControllerScript PC;
    ToggleInspectElementChildren ToggleSprites;
    //public Image WaterLevelSprite;
    //public Image BackgroundSprite;
    //public Image RootsSprite;
    //public Image ContainerSprite;

    private void Start()
    {
        this.PC = GetComponent<PlantControllerScript>();
        this.ToggleSprites = FindObjectOfType<ToggleInspectElementChildren>();
    }

    private void OnMouseOver()
    {
        if (ToolManagerScript.Instance.CheckActiveTool(ToolManagerScript.Tools.Inspect))
        {
            if(PC._Plant != null && ToggleSprites != null)
            {
                int waterFactor = PC.GetRootWaterFactor();

                if (waterFactor == 1)
                {
                    //WaterLevelSprite.GetComponent<RectTransform>().localPosition = new Vector3(0, 245, 0);
                    ToggleSprites.ToggleElementsOn(new Vector3(0, 245, 0));
                }
                else if (waterFactor == 0)
                {
                    //WaterLevelSprite.GetComponent<RectTransform>().localPosition = new Vector3(0, 205, 0);
                    ToggleSprites.ToggleElementsOn(new Vector3(0, 205, 0));
                }
                else if (waterFactor == -1)
                {
                    //WaterLevelSprite.GetComponent<RectTransform>().localPosition = new Vector3(0, 155, 0);
                    ToggleSprites.ToggleElementsOn(new Vector3(0, 155, 0));
                }

                //ToggleSprites(true);
            }
        }
    }

    private void OnMouseExit()
    {
        if (ToolManagerScript.Instance.CheckActiveTool(ToolManagerScript.Tools.Inspect))
        {
            //ToggleSprites(false);
            ToggleSprites.ToggleELementsOff();
        }
    }
}
