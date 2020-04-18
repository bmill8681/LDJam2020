using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlantStuff;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance { get; private set; }
   
    public string scene;

    public GameTimer GameTimeController = null;

    public List<Plant> PlantList;

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
        // If it's time to run an assessment on the plants, assess all plants in the scene
        // This means we need a list of all the living plants in the scene
        if (GameTimeController.GetUpdateStatus())
        {
            RunAssessmentOnPlants();
        }
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
            plant.PrintPlantStatus();

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
