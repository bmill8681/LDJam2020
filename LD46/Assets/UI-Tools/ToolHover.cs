using PlantTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolHover : MonoBehaviour
{
    private float _activeHeight = 0.0f;
    private float _inactiveHeight;
    public bool IsActive = false;

    public ToolManagerScript.Tools ToolType;

    private void Start()
    {
        _inactiveHeight = GetComponent<RectTransform>().position.y;
        UpdateActiveState();
    }
    private void Update()
    {
        UpdateActiveState();
    }

    private void UpdateActiveState()
    {

        if (ToolManagerScript.Instance.CheckActiveTool(this.ToolType))
        {
            if (!IsActive)
            {
                SetHovered();
                IsActive = true;
            }
        }
        else
        {
            if (IsActive)
            {
                IsActive = false;
                SetNotHovered();
            }
        }
    }

    private void SetHovered()
    {
        RectTransform transform = GetComponent<RectTransform>();
        transform.position = new Vector2(transform.position.x, _activeHeight);
    }

    private void SetNotHovered()
    {
        RectTransform transform = GetComponent<RectTransform>();
        transform.position = new Vector2(transform.position.x, _inactiveHeight);
    }

    public void SetActive()
    {
        ToolManagerScript.Instance.SetActiveTool(this.ToolType);
    }
}
