using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlantTools
{
    public class PlantToolManager : MonoBehaviour
    {
        public enum Tools
        {
            Hand = 0,
            WateringCan = 1,
            Sheers = 2,
            Shovel = 3,
        }

        Tools ActiveTool = Tools.Hand;

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

