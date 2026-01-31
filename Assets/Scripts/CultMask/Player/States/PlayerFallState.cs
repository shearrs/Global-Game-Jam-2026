using UnityEngine;

namespace CultMask.Players
{
    [System.Serializable]
    public class PlayerFallState : PlayerState
    {
        private float verticalVelocity;

        public PlayerFallState()
        {
            Name = "Fall";
        }

        protected override void OnEnter()
        {
            verticalVelocity = Controller.Velocity.y;
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
            Controller.Move(Time.deltaTime * verticalVelocity * Vector3.up);

            verticalVelocity = Controller.Velocity.y;
            verticalVelocity += Data.Gravity * Time.deltaTime;

            StandardUpdateMovement();
        }
    }
}
