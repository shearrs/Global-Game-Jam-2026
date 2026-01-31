using Shears;
using UnityEngine;

namespace CultMask.Players
{
    [System.Serializable]
    public class PlayerJumpState : PlayerState
    {
        private static readonly Timer MIN_JUMP_TIMER = new(0.1f);

        private bool jumpHeld;

        public PlayerJumpState()
        {
            Name = "Jump";
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

            StandardUpdateMovement();
        }

        private void ApplyJumpForce()
        {
            float jumpForce = (Flags.CanDashJump) ? Data.DashJumpForce : Data.JumpForce;

            Controller.SetVelocity(y: jumpForce);
            jumpHeld = true;
            MIN_JUMP_TIMER.Start();
        }
    }
}
