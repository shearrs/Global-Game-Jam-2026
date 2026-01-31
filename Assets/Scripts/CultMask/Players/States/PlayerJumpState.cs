using Shears;
using UnityEngine;

namespace CultMask.Players
{
    [System.Serializable]
    public class PlayerJumpState : PlayerState
    {
        private static readonly Timer MIN_JUMP_TIMER = new(0.1f);

        private float verticalVelocity;
        private bool jumpHeld;

        public PlayerJumpState()
        {
            Name = "Jump";
        }

        protected override void OnEnter()
        {
            verticalVelocity = Data.JumpForce;
            jumpHeld = true;
            MIN_JUMP_TIMER.Start();
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
            var jumpMovement = new Vector3(0, verticalVelocity, 0) * Time.deltaTime;
            Controller.Move(jumpMovement);

            verticalVelocity = Mathf.Min(verticalVelocity, Controller.Velocity.y);

            if (!Input.JumpInput.IsPressed())
                jumpHeld = false;

            if (!jumpHeld && MIN_JUMP_TIMER.IsDone)
                verticalVelocity += Data.FastFallGravity * Time.deltaTime;
            else
                verticalVelocity += Data.Gravity * Time.deltaTime;

            StandardUpdateMovement();
        }
    }
}
