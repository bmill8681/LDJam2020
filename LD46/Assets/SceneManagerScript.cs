using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public string GetCurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void QuitGame()
    {
        Debug.Log("I have quit the game");
        Application.Quit();
    }

    public void StartGame()
    {
        Debug.Log("I am starting the game");
        SceneManager.LoadScene("Game");
    }

    public void EndGame()
    {
        Debug.Log("I am entering the end of the game");
        SceneManager.LoadScene("End");
    }

    public void MainMenu()
    {
        Debug.Log("I am returning to the main menu");
        SceneManager.LoadScene("MainMenu");
    }
}
