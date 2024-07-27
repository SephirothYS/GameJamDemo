using System.Collections;
using System.Collections.Generic;
using TbsFramework.Cells;
using TbsFramework.Units;
using UnityEngine;
using TbsFramework.Fog;
using TbsFramework.Grid;
using TbsFramework.CA;

namespace TbsFramework.HOMMExample
{
    public class HOMMUnit : Unit
    {
        public Vector3 offset;
        public bool IsHero;

        public string UnitName;
        public FogManager fogManager;
        public CharacterAnimator characterAnimator;
        public float Oxygen = 100;
        public GameObject gameOverUIPanel;
        public CanvasGroup blackScreenCanvasGroup;
        public CanvasGroup UICanvasGroup;

        private void Start()
        {
            fogManager.UpdateVisibility();
            blackScreenCanvasGroup.alpha = 0;
        }
        public override void Initialize()
        {
            base.Initialize();
            transform.position += offset;
        }

        protected override void OnMoveFinished()
        {
            //GetComponentInChildren<SpriteRenderer>().sortingOrder = (int)(Cell.OffsetCoord.x * Cell.OffsetCoord.y);
            if (Oxygen <= 0)
            {
                TriggerGameOver();
            }
            else
            { 
                fogManager.UpdateVisibility();
            }
        }

        public override bool IsUnitAttackable(Unit other, Cell otherCell, Cell sourceCell)
        {
            return otherCell != null && base.IsUnitAttackable(other, otherCell, sourceCell);
        }

        public override void OnMouseDown()
        {
            base.OnMouseDown();
        }

        public override IEnumerator Move(Cell destinationCell, IList<Cell> path)
        {
            IEnumerator Res = base.Move(destinationCell, path);
            return Res;
        }

        protected override IEnumerator MovementAnimation(IList<Cell> path)
        {
            for (int i = path.Count - 1; i >= 0; i--)
            {
                var currentCell = path[i];
                Vector3 destination_pos = FindObjectOfType<CellGrid>().Is2D ? new Vector3(currentCell.transform.localPosition.x, currentCell.transform.localPosition.y, transform.localPosition.z) : new Vector3(currentCell.transform.localPosition.x, currentCell.transform.localPosition.y, currentCell.transform.localPosition.z);
                while (transform.localPosition != destination_pos)
                {
                    UpdateAnimation(currentCell);
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination_pos, Time.deltaTime * MovementAnimationSpeed);
                    yield return null;
                }
                previousCell = currentCell;
                Oxygen -= currentCell.MovementCost;
            }
            SetIdleAnimation();
            OnMoveFinished();
        }


        private void UpdateAnimation(Cell targetCell)
        {
            string animationState = GetAnimationState(targetCell);
            characterAnimator.ChangeAnimationState(animationState);
        }

        private string GetAnimationState(Cell targetCell)
        {
            HOMMHex pCell = previousCell as HOMMHex;
            int direction = pCell.GetNeighbourDirection(targetCell as HOMMHex);
            if (direction >= 0 && direction < 3) return "WalkRight";
            else return "WalkLeft";
        }

        private void SetIdleAnimation()
        {
            string currentAnimation = characterAnimator.currentState;
            string idleAnimation = currentAnimation.Replace("Walk", "Idle");
            characterAnimator.ChangeAnimationState(idleAnimation);

        }

        void TriggerGameOver()
        {
            StartCoroutine(GameOverSequence());
        }

        IEnumerator GameOverSequence()
        {
            // Gradually darken the screen
            float alpha = 0f;

            while (alpha < 1f)
            {
                alpha += Time.deltaTime / 2;  // Duration of the black screen effect
                blackScreenCanvasGroup.alpha = alpha;
                yield return null;
            }

            // Optionally, wait for a bit before displaying "Game Over"
            yield return new WaitForSeconds(1f);

            // Show Game Over UI
            gameOverUIPanel.SetActive(true);
            UICanvasGroup.alpha = 1f;
        }
    }
}