using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TbsFramework.Units;

public class CharacterUnit : Unit
{
    public string UnitName;
    public Vector3 offset;
    public override void Initialize()
    {
        base.Initialize();
        transform.position += offset;
    }

    public override void OnMouseDown()
    {
        base.OnMouseDown();
    }
}
