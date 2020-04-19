using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlantStuff;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance { get; private set; }
   
    public string scene;

    public GameTimer GameTimeController = null;
    public PlantCounterScript PlantCounter;

    public List<Plant> PlantList;
    private int PrevPlantCount = 0;

    public bool ToolsDisabled = true;

    

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
        //setInitialMusic();
        scene = SceneManagerScript.Instance.GetCurrentScene();
    }

    private void Update()
    {
        if(SceneManagerScript.Instance.GetCurrentScene().Equals("Game") && GameTimeController == null)
        {
            GameTimeController = FindObjectOfType<GameTimer>();
        }
        if(SceneManagerScript.Instance.GetCurrentScene().Equals("Game") && PlantCounter == null)
        {
            PlantCounter = FindObjectOfType<PlantCounterScript>();
        }
        // If it's time to run an assessment on the plants, assess all plants in the scene
        // This means we need a list of all the living plants in the scene
        if (GameTimeController != null && GameTimeController.GetUpdateStatus())
        {
            RunAssessmentOnPlants();
        }

        if(PrevPlantCount != PlantList.Count)
        {
            PlantCounter.UpdatePlantCounter(PlantList.Count);
        }
    }

    public void ResetGame()
    {
        this.PlantList = new List<Plant>();
        this.ToolsDisabled = true;
        if (GameTimeController != null)
        {
            GameTimeController.ResetGame();
        }
    }

    public void ToggleToolsDisabled()
    {
        ToolsDisabled = !ToolsDisabled;
    }

    public void SetToolsDisabled(bool value)
    {
        ToolsDisabled = value;
    }

    private void setInitialMusic()
    {
        string scene = GameObject.FindObjectOfType<SceneManagerScript>().GetCurrentScene(); 
        AudioManagerScript.Instance.SetMusic(string.Format("{0}Music", scene));
    }

    private void RunAssessmentOnPlants()
    {
        foreach(Plant plant in PlantList)
        {
            if (!plant.IsPlanted)
            {
                plant.RemoveHealth();
            }
            else
            {
                PlantControllerScript PC = plant.GetComponentInParent<PlantControllerScript>();
                PC.AssessPlant();
            }
        }
        GameTimeController.SetRunUpdateFalse();
    }

    public void AddPlantTolist(Plant plant)
    {
        this.PlantList.Add(plant);
    }

    public void AttachGameTimer(GameTimer timer)
    {
        this.GameTimeController = timer;
    }
}
