using PlantTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectPlanterTool : MonoBehaviour
{
    PlantControllerScript PC;
    public Image WaterLevelSprite;
    public Image BackgroundSprite;
    public Image RootsSprite;
    public Image ContainerSprite;

    private void Start()
    {
        this.PC = GetComponent<PlantControllerScript>();
    }

    private void OnMouseOver()
    {
        if (ToolManagerScript.Instance.CheckActiveTool(ToolManagerScript.Tools.Inspect))
        {
            if(PC._Plant != null)
            {
                int waterFactor = PC.GetRootWaterFactor();

                if (waterFactor == 1)
                {
                    WaterLevelSprite.GetComponent<RectTransform>().localPosition = new Vector3(0, 245, 0);
                }
                else if (waterFactor == 0)
                {
                    WaterLevelSprite.GetComponent<RectTransform>().localPosition = new Vector3(0, 205, 0);
                }
                else if (waterFactor == -1)
                {
                    WaterLevelSprite.GetComponent<RectTransform>().localPosition = new Vector3(0, 155, 0);
                }

                ToggleSprites(true);
            }
        }
    }

    private void OnMouseExit()
    {
        ToggleSprites(false);
    }

    private void ToggleSprites(bool val)
    {
        WaterLevelSprite.enabled = val;
        BackgroundSprite.enabled = val;
        RootsSprite.enabled = val;
        ContainerSprite.enabled = val;
    }
}
