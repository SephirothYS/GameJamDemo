using TbsFramework.Cells;
using UnityEngine;
using TbsFramework.Cells.Highlighters;
using System.Collections.Generic;

namespace TbsFramework
{
    public class HOMMHex : Hexagon
    {
        public List<CellHighlighter> MarkAsOccypiedFn;
        public override Vector3 GetCellDimensions()
        {
            return new Vector3(5.34f, 4.6f, 0f);
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

        public override void MarkAsHighlighted()
        {
            if (!HasFog)
            {
                base.MarkAsHighlighted();
            }
        }

        public void MarkAsOccupied()
        {
            MarkAsOccypiedFn?.ForEach(o => o.Apply(this));
        }

        public int GetNeighbourDirection(HOMMHex other)
        {
            if (other == null) return -1;
            for (int i = 0; i < _directions.Length; i++)
            {
                if (other.OffsetCoord == CubeToOffsetCoords(CubeCoord + _directions[i]))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
