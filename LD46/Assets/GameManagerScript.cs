using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance { get; private set; }

    public string scene;

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
    private void Start()
    {
        setInitialMusic();
    }

    private void setInitialMusic()
    {
        string scene = GameObject.FindObjectOfType<SceneManagerScript>().GetCurrentScene();
        AudioManagerScript.Instance.SetMusic(string.Format("{0}Music", scene));
    }
}
