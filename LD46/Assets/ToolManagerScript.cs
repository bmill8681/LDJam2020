using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlantTools
{
    public class ToolManagerScript : MonoBehaviour
    {
        public static ToolManagerScript Instance { get; private set; }
        public enum Tools
        {
            Hand = 0,
            WateringCan = 1,
            Sheers = 2,
            Shovel = 3,
            Inspect = 4
        }

        Tools ActiveTool;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                ActiveTool = Tools.Hand;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public Tools GetActiveTool()
        {
            return ActiveTool;
        }

        public bool CheckActiveTool(Tools tool)
        {
            return ActiveTool == tool;
        }

        public void SetActiveTool(Tools tool)
        {
            ActiveTool = tool;
        }
    }
}

