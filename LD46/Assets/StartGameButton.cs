using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    public void StartTheGame()
    {
        SceneManagerScript.Instance.StartGame();
    }

    public void QuitTheGame()
    {
        SceneManagerScript.Instance.QuitGame();
    }
}
