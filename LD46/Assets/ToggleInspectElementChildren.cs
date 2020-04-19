using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleInspectElementChildren : MonoBehaviour
{
    public Image WaterLevelSprite;
    public Image BackgroundSprite;
    public Image RootsSprite;
    public Image ContainerSprite;

    public void ToggleElementsOn(Vector3 waterPos)
    {
        WaterLevelSprite.GetComponent<RectTransform>().localPosition = waterPos;
        ToggleSprites(true);
    }

    public void ToggleELementsOff()
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
