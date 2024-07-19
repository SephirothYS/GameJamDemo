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

        private void Start()
        {
            fogManager.UpdateVisibility();
        }
        public override void Initialize()
        {
            base.Initialize();
            transform.position += offset;
        }

        protected override void OnMoveFinished()
        {
            //GetComponentInChildren<SpriteRenderer>().sortingOrder = (int)(Cell.OffsetCoord.x * Cell.OffsetCoord.y);
            fogManager.UpdateVisibility();
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
    }
}