using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanelHandler : MonoBehaviour
{
    private float InitialScale = 4.55f;
    private float MinimumScale = 0.5f;
    private float CurrentScale;
    public float Increment;
    public float InitialIncrement = 0;
    [SerializeField]
    private float MaximumIncrement = 0.1f;
    private bool AnimateIn = false;
    private bool DoneAnimating = false;
    public Transform trans;


    private void Start()
    {
        this.CurrentScale = this.InitialScale;
    }

    void Update()
    {
        if (AnimateIn && !DoneAnimating)
        {
            if(Increment < MaximumIncrement)
            {
                Increment += Time.deltaTime / 100;
            }
            CurrentScale -= Increment;
            trans.localScale = new Vector3(CurrentScale, CurrentScale, 1);
            if(CurrentScale < MinimumScale)
            {
                CurrentScale = MinimumScale;
                DoneAnimating = true;
            }
        }
        if (GameManagerScript.Instance.GameIsOver && !AnimateIn && !DoneAnimating)
        {
            this.TriggerGameOverPanel(GameManagerScript.Instance.GetDeadPlantTransform());
        }
    }

    public void ResetScale()
    {
        this.CurrentScale = this.InitialScale;
        this.AnimateIn = false;
        this.DoneAnimating = false;
        this.Increment = this.InitialIncrement;
        trans.position = new Vector3(0, 0, -1);
    }

    private void TriggerGameOverPanel(Transform newTransform)
    {
        trans.position = new Vector3 (newTransform.position.x, newTransform.position.y - 0.05f, newTransform.position.z);
        this.AnimateIn = true;
    }
}
