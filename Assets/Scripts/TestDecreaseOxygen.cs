using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TbsFramework.HOMMExample;

public class TestDecreaseOxygen : MonoBehaviour
{
    public HOMMUnit character;

    public void OnClicked()
    {
        character.Oxygen -= 10;
    }
}
