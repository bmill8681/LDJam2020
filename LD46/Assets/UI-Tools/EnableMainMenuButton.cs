using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMainMenuButton : MonoBehaviour
{
    public GameObject MainMenuButton;
    private bool Visibility = true;

    public void ToggleMainMenuButtonVisibility()
    {
        Visibility = !Visibility;
        MainMenuButton.SetActive(Visibility);
    }
}
