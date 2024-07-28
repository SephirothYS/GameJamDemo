using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIOpenNote : MonoBehaviour
{
    public GameObject targetCanvas;

    public void Start()
    {
        targetCanvas.SetActive(false);
    }

    public void ToggleCanvas()
    {
        if (targetCanvas == null) return;
        targetCanvas.SetActive(!targetCanvas.activeSelf);
    }

    public void OnButtonClicked()
    {
        ToggleCanvas();    
    }
}
