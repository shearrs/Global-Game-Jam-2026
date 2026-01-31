using UnityEngine;

namespace CultMask.Players
{
    [System.Serializable]
    public class PlayerJumpState : PlayerState
    {
        private float verticalVelocity;

        public PlayerJumpState()
        {
            Name = "Jump";
        }

        protected override void OnEnter()
        {
            verticalVelocity = Data.JumpForce;
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
            var jumpMovement = new Vector3(0, verticalVelocity, 0) * Time.deltaTime;
            Controller.Move(jumpMovement);

            verticalVelocity = Mathf.Min(verticalVelocity, Controller.Velocity.y);
            verticalVelocity += Data.Gravity * Time.deltaTime;
            StandardUpdateMovement();
        }
    }
}
