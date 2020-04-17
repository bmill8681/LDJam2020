using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("I have quit the game");
        Application.Quit();
    }

    public void StartGame()
    {
        Debug.Log("I am starting the game");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void EndGame()
    {
        Debug.Log("I am entering the end of the game");
        UnityEngine.SceneManagement.SceneManager.LoadScene("End");
    }

    public void MainMenu()
    {
        Debug.Log("I am returning to the main menu");
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
