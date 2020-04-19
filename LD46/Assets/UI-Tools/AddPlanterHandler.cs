using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlanterHandler : MonoBehaviour
{
    public GameObject NewPlanter;
    public Transform NewPlanterPosition;

    public void AddNewPlanter()
    {
        Debug.Log("Adding a new planter");
        //GameObject newPlanter = Instantiate(NewPlanter) as GameObject;
        //newPlanter.transform.position = NewPlanterPosition.position;
    }
}
