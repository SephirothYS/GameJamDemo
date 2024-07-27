using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundController : MonoBehaviour
{
    public List<Image> backgroundImages;  // Assign this in the inspector

    void Start()
    {
        // Ensure the background image is initially not visible
        foreach (Image img in backgroundImages)
        { 
            img.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
