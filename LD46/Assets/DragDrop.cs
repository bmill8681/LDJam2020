using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public bool IsDragging = false;
    Vector3 mousePosition;
    float mZCoordinate;

    //private void OnMouseDown()
    //{
    //    mousePosition = Camera.main.WorldToScreenPoint(this.transform.position);
    //    Debug.Log(mousePosition);
    //}

    //private void OnMouseDrag()
    //{
    //    this.transform.position = GetMouseAsWorldPoint();
    //    Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    //}

    //Vector3 GetMouseAsWorldPoint()
    //{
    //    return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //}


    private void OnMouseDown()
    {
        mZCoordinate = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mousePosition = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoordinate;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mousePosition;
    }
}
