using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlantStuff.Plant;

public class PlantSpriteUpdater : MonoBehaviour
{
    public List<Sprite> HP3_PlantSpriteList;
    public List<Sprite> HP2_PlantSpriteList;
    public List<Sprite> HP1_PlantSpriteList;
    public List<Sprite> HP0_PlantSpriteList;

    // HP has a max of 6
    // HP + 1 / 2 gives the visual for the plant health
    public void SetPlantSprite(PlantSizes size, int HP)
    {
        int index = (int)size / 4 - 1;
        int modHP = (HP + 1) / 2;
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        switch (modHP)
        {
            case 3:
                SetSpriteHP3(spriteRenderer, index);
                break;
            case 2:
                SetSpriteHP2(spriteRenderer, index);
                break;
            case 1:
                SetSpriteHP1(spriteRenderer, index);
                break;
            case 0:
                SetSpriteHP0(spriteRenderer, index);
                break;
        }
    }

    private void SetSpriteHP3(SpriteRenderer spriteRenderer, int index)
    {
        spriteRenderer.sprite = HP3_PlantSpriteList[index];
    }

    private void SetSpriteHP2(SpriteRenderer spriteRenderer, int index)
    {
        spriteRenderer.sprite = HP2_PlantSpriteList[index];
    }

    private void SetSpriteHP1(SpriteRenderer spriteRenderer, int index)
    {
        spriteRenderer.sprite = HP1_PlantSpriteList[index];
    }

    private void SetSpriteHP0(SpriteRenderer spriteRenderer, int index)
    {
        spriteRenderer.sprite = HP0_PlantSpriteList[index];
    }
}
