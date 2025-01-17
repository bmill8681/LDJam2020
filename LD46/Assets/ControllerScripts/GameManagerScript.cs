﻿using System.Collections;
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
    private Plant DeadPlant = null;
    private int PrevPlantCount = 0;

    public bool ToolsDisabled = true;
    public bool GameIsOver = false;



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
        scene = SceneManagerScript.Instance.GetCurrentScene();
    }

    private void Update()
    {
        if (!GameIsOver)
        {
            if (SceneManagerScript.Instance.GetCurrentScene().Equals("Game") && GameTimeController == null)
            {
                GameTimeController = FindObjectOfType<GameTimer>();
            }
            if (SceneManagerScript.Instance.GetCurrentScene().Equals("Game") && PlantCounter == null)
            {
                PlantCounter = FindObjectOfType<PlantCounterScript>();
            }
            // If it's time to run an assessment on the plants, assess all plants in the scene
            // This means we need a list of all the living plants in the scene
            if (GameTimeController != null && GameTimeController.GetUpdateStatus())
            {
                RunAssessmentOnPlants();
            }

            if (PrevPlantCount != PlantList.Count)
            {
                PlantCounter.UpdatePlantCounter(PlantList.Count);
            }
        }
    }

    public void ResetGame()
    {
        this.PlantList = new List<Plant>();
        this.ToolsDisabled = true;
        this.GameIsOver = false;
        this.DeadPlant = null;
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
        AudioManagerScript.Instance.SetMusic("BadMusic");
    }

    private void RunGameOver()
    {
        GameOverPanelHandler goPanelHandler = FindObjectOfType<GameOverPanelHandler>();
        GameObject removeNext = null;
        foreach(Plant plant in PlantList)
        {
            if(plant != DeadPlant)
            {
                removeNext = plant.GetComponent<GameObject>();
            }
            if(plant != DeadPlant && plant != removeNext)
            {
                Destroy(removeNext);
            }
        }
        if(removeNext != null)
        {
            Destroy(removeNext);
        }
    }

    public Transform GetDeadPlantTransform()
    {
        return this.DeadPlant.GetComponent<Transform>();
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
            if (plant.IsDead)
            {
                return;
            }
        }
        GameTimeController.SetRunUpdateFalse();
    }

    public void SetDeadPlant(Plant plant)
    {
        this.GameIsOver = true;
        this.DeadPlant = plant;
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
