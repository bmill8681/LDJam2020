using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthManager : MonoBehaviour
{
    private float MaxHeight = 0.2f;
    private float MinHeight = 0.0f;
    private float MaxWidth = 1.0f;
    private float MinWidth = -1.0f;
    private float DepthFactor = 10.0f;

    void Update()
    {
        Transform transform = gameObject.GetComponent<Transform>();
        ConstrainHeight(transform);
        ConstrainWidth(transform);
        AdjustDepth(transform);
    }

    private void ConstrainHeight(Transform transform)
    {
        if(transform.position.y > this.MaxHeight)
        {
            transform.position = new Vector3(transform.position.x, this.MaxHeight, transform.position.z);
        }
        else if (transform.position.y < this.MinHeight)
        {
            transform.position = new Vector3(transform.position.x, this.MinHeight, transform.position.z);
        }
    }

    private void ConstrainWidth(Transform transform)
    {
        if (transform.position.x > this.MaxWidth)
        {
            transform.position = new Vector3(this.MaxWidth, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < this.MinWidth)
        {
            transform.position = new Vector3(this.MinWidth, transform.position.y, transform.position.z);
        }
    }

    public void AdjustDepth(Transform transform)
    {
        // The height goes from 0 to 0.2
        // The Z Index goes from 0 to 2
        // multiply by a factor of 10;
        float newDepth = transform.position.y * this.DepthFactor;
        transform.position = new Vector3(transform.position.x, transform.position.y, newDepth);
    }
}
