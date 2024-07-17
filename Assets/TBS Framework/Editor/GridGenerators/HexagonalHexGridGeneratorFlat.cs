using System.Collections.Generic;
using TbsFramework.Cells;
using UnityEditor;
using UnityEngine;

namespace TbsFramework.EditorUtils.GridGenerators
{
    /// <summary>
    /// Generates hexagonal shaped grid of hexagons.
    /// </summary>
    [ExecuteInEditMode()]
    class FlatHexagonalHexGridGenerator : ICellGridGenerator
    {
#pragma warning disable 0649
        public GameObject HexagonPrefab;
        public int Radius;
        public float gatherFactor;
        public float xFactor;
        public float yFactor;
#pragma warning restore 0649

        public override GridInfo GenerateGrid()
        {
            var hexagons = new List<Cell>();

            if (HexagonPrefab.GetComponent<Hexagon>() == null)
            {
                Debug.LogError("Invalid hexagon prefab provided");
                return null;
            }

            var hexSize = HexagonPrefab.GetComponent<Cell>().GetCellDimensions();

            // 计算网格的中心点
            Vector3 centerPoint = new Vector3((Radius - 1) * hexSize.x * 0.5f, 0, 0);

            for (int i = 0; i < Radius; i++)
            {
                for (int j = 0; j < (Radius * 2) - i - 1; j++)
                {
                    var hexagon = PrefabUtility.InstantiatePrefab(HexagonPrefab) as GameObject;

                    // 计算初始位置
                    var initialPosition = Is2D ?
                        new Vector3((i * hexSize.x * xFactor) + (j * hexSize.x), (i * hexSize.y * yFactor)) :
                        new Vector3((i * hexSize.x * xFactor) + (j * hexSize.x), 0, (i * hexSize.z * yFactor));

                    // 计算到中心的向量
                    Vector3 toCenterVector = centerPoint - initialPosition;

                    // 应用聚拢效果
                    Vector3 position = initialPosition + toCenterVector * gatherFactor;

                    hexagon.transform.position = position;
                    hexagon.GetComponent<Hexagon>().OffsetCoord = new Vector2(Radius - j - 1 - (i / 2), i);
                    hexagon.GetComponent<Hexagon>().HexGridType = HexGridType.odd_r;
                    hexagon.GetComponent<Hexagon>().MovementCost = 1;
                    hexagons.Add(hexagon.GetComponent<Cell>());

                    hexagon.transform.parent = CellsParent;

                    if (i == 0) continue;

                    var hexagon2 = PrefabUtility.InstantiatePrefab(HexagonPrefab) as GameObject;

                    // 计算第二个六边形的初始位置
                    initialPosition = Is2D ?
                        new Vector3((i * hexSize.x * xFactor) + (j * hexSize.x), (-i * hexSize.y * yFactor)) :
                        new Vector3((i * hexSize.x * xFactor) + (j * hexSize.x), 0, (-i * hexSize.z * yFactor));

                    // 计算到中心的向量
                    toCenterVector = centerPoint - initialPosition;

                    // 应用聚拢效果
                    position = initialPosition + toCenterVector * gatherFactor;

                    hexagon2.transform.position = position;
                    hexagon2.GetComponent<Hexagon>().OffsetCoord = new Vector2(Radius - j - 1 - (i / 2), -i);
                    hexagon2.GetComponent<Hexagon>().HexGridType = HexGridType.odd_r;
                    hexagon2.GetComponent<Hexagon>().MovementCost = 1;
                    hexagons.Add(hexagon2.GetComponent<Cell>());

                    hexagon2.transform.parent = CellsParent;
                }
            }

            return GetGridInfo(hexagons);
        }
    }
}