using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TbsFramework.Cells;
using TbsFramework.Grid;
using TbsFramework.HOMMExample;

namespace TbsFramework.Fog
{ 
    public class FogManager : MonoBehaviour
    {
        public CellGrid cellGrid;
        public HOMMUnit character;
        public int visibilityRange;
        private List<Cell> cells;
        public float dissolveDuration = 3f;
        // Start is called before the first frame update
        void Start()
        {
            cells = cellGrid.Cells;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void loadCellGrid(CellGrid cellGrid)
        {
            cells = cellGrid.Cells;
        }
        public void UpdateVisibility()
        {
            if (cells == null)
            {
                loadCellGrid(cellGrid);
            }
            Cell characterCell = character.Cell;
            if (cells == null) return;
            foreach (Cell cell in cells)
            {
                if (characterCell.GetDistance(cell) <= visibilityRange)
                {
                    RevealHex(cell);
                }
            }
        }

        public void RevealHex(Cell cell)
        {
            cell.HasFog = false;
            Transform fog = cell.transform.Find("Fog");
            if (!fog) return;
            Material fogMaterial = fog.GetComponent<SpriteRenderer>().material;
            if (!fogMaterial) return;
            StartCoroutine(DissolveEffect(fogMaterial));
        }

        private IEnumerator DissolveEffect(Material material)
        {
            float elapsedTime = 0f;
            float curDissolveAmount = material.GetFloat("_DissolveAmount");
            while (elapsedTime < dissolveDuration)
            {
                elapsedTime += Time.deltaTime;
                float dissolveAmount = Mathf.Clamp01(elapsedTime / dissolveDuration) * (1f - curDissolveAmount) + curDissolveAmount;
                material.SetFloat("_DissolveAmount", dissolveAmount);
                yield return null;
            }
        }

    }

}

