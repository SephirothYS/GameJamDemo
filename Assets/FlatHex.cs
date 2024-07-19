using TbsFramework.Cells;
using UnityEngine;

namespace TbsFramework.HOMMExample
{
    public class FlatHex : Hexagon
    {
        //new protected static readonly Vector3[] _directions = {
        //new Vector3(1, 0, -1),  //сроб
        //new Vector3(1, -1, 0),  //вС
        //new Vector3(0, -1, 1),  //срио
        //new Vector3(-1, 0, 1),  //сриоср
        //new Vector3(-1, 1, 0),  //ср
        //new Vector3(0, 1, -1)}; //сробср
        public override Vector3 GetCellDimensions()
        {
            return new Vector3(4.6f, 5.34f, 0f);
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

        //public override List<Cell> GetNeighbours(List<Cell> cells)
        //{
        //    if (neighbours == null)
        //    {
        //        neighbours = new List<Cell>(6);
        //        foreach (var direction in _directions)
        //        {
        //            var neighbour = cells.Find(c => c.OffsetCoord == CubeToOffsetCoords(CubeCoord + direction));
        //            if (neighbour == null) continue;
        //            neighbours.Add(neighbour);
        //        }
        //    }
        //    return neighbours;
        //}
    }
}
