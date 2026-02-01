using UnityEngine;

namespace CultMask.Players
{
    [System.Serializable]
    public class PlayerFallState : PlayerState
    {
        public PlayerFallState()
        {
            Name = "Fall";
        }

        protected override void OnEnter()
        {
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
            ApplyGravity();

            AdaptiveUpdateMovement();
        }
    }
}
