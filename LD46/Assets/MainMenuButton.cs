using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    public void QuitToMainMenu()
    {
        GameManagerScript.Instance.ResetGame();
        SceneManagerScript.Instance.MainMenu();
    }
}
