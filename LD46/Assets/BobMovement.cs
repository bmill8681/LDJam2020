using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BobMovement : MonoBehaviour
{
    public float DistanceFromCenter;
    public float Increment;
    [SerializeField]
    private float InitialHeight;
    private bool MoveUp;
    private bool Visible;
    public RectTransform trans;
    public Image sprite;

    private void Start()
    {
        InitialHeight = trans.localPosition.y;
        MoveUp = true;
        Visible = true;
    }

    private void Update()
    {
        if (Visible)
        {
            HandleBob();
        }
    }
    public void ToggleVisible()
    {
        this.Visible = !this.Visible;
        sprite.enabled = this.Visible;
    }
    private void HandleBob()
    {
        if (MoveUp)
        {
            if (trans.localPosition.y < InitialHeight + DistanceFromCenter)
            {
                trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y + Increment, trans.localPosition.z);
            }
            else
            {
                MoveUp = false;
            }
        }
        else
        {
            if (trans.localPosition.y > InitialHeight - DistanceFromCenter)
            {
                trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y - Increment, trans.localPosition.z);
            }
            else
            {
                MoveUp = true;
            }
        }
    }
}
