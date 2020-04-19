using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlanterHandler : MonoBehaviour
{
    public GameObject NewPlanter;
    public Transform NewPlanterPosition;

    public void AddNewPlanter()
    {
        if (!GameManagerScript.Instance.ToolsDisabled)
        {
            GameObject newPlanter = Instantiate(NewPlanter) as GameObject;
            newPlanter.transform.position = NewPlanterPosition.position;
        }
    }
}
