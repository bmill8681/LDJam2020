using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverTextHandler : MonoBehaviour
{
    public TextMeshProUGUI TextObject;
    void Update()
    {
        if (!TextObject.enabled && GameManagerScript.Instance.GameIsOver)
        {
            TextObject.enabled = true;
        }
        else if (TextObject.enabled && !GameManagerScript.Instance.GameIsOver)
        {
            TextObject.enabled = false;
        }
    }
}
