using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using PlantTools;

public class DragDrop : MonoBehaviour
{

    public bool IsDragging = false;
    Vector3 mousePosition;
    float mZCoordinate;

    private void OnMouseUp()
    {
        IsDragging = false;
    }

    private void OnMouseDown()
    {
        if (ToolManagerScript.Instance.GetActiveTool() != ToolManagerScript.Tools.Hand) return;

        mZCoordinate = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mousePosition = gameObject.transform.position - GetMouseWorldPos();
        IsDragging = true;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoordinate;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        if (ToolManagerScript.Instance.GetActiveTool() != ToolManagerScript.Tools.Hand) return;

        transform.position = GetMouseWorldPos() + mousePosition;
    }
}
