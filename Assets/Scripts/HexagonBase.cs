using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TbsFramework.Cells;

public class HexagonBase : Hexagon
{
    public override Vector3 GetCellDimensions()
    {
        // return new Vector3(1.6f, 1.6f, 0f);
        return GetComponent<SpriteRenderer>().bounds.size;
    }
    public override int GetDistance(Cell other)
    {
        return other == null ? int.MaxValue : base.GetDistance(other);
    }

    public override bool Equals(Cell other)
    {
        return other != null && base.Equals(other);
    }

    public override void SetColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }
}
