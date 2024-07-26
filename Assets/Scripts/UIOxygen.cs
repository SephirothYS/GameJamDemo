using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TbsFramework.HOMMExample;

public class UIOxygen : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI uiText;
    public HOMMUnit character;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (character != null)
        {
           uiText.text = "Oxygen: " + character.Oxygen.ToString();
        }
    }
}
