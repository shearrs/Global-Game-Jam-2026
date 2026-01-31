using Shears;
using UnityEngine;

namespace CultMask.Players
{
    [System.Serializable]
    public class PlayerDoubleJumpState : PlayerState
    {
        private static readonly Timer MIN_JUMP_TIMER = new();


        private bool jumpHeld;

        public PlayerDoubleJumpState()
        {
            Name = "Double Jump";
        }

        protected override void OnEnter()
        {
            ApplyJumpForce();
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
            if (!Input.JumpInput.IsPressed())
                jumpHeld = false;

            if (!jumpHeld && MIN_JUMP_TIMER.IsDone)
                Controller.AddVelocity(Data.FastFallGravity * Time.deltaTime * Vector3.up);
            else
                ApplyGravity();

            if (Flags.HasDashed)
                AfterDashUpdateMovement();
            else
                StandardUpdateMovement();
        }

        private void ApplyJumpForce()
        {
            Controller.SetVelocity(y: Data.DoubleJumpForce);
            jumpHeld = true;
            MIN_JUMP_TIMER.Start(Data.MinimumJumpTime);
        }
    }
}
