using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlantStuff.Plant;

public class PlantSpriteUpdater : MonoBehaviour
{
    public List<Sprite> PlantSpriteList;

    public void SetPlantSprite(PlantSizes size)
    {
        Debug.Log("Setting sprite size: " + size);
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        int index = (int)size / 4 - 1;
        spriteRenderer.sprite = PlantSpriteList[index];
    }
}
