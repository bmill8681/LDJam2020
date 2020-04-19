using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMainMenuButton : MonoBehaviour
{
    public GameObject MainMenuButton;
    private bool Visibility = true;

    private void Update()
    {
        if (GameManagerScript.Instance.GameIsOver && !Visibility)
        {
            this.ToggleMainMenuButtonVisibility();
        }
    }
    public void ToggleMainMenuButtonVisibility()
    {
        Visibility = !Visibility;
        MainMenuButton.SetActive(Visibility);
    }
}
