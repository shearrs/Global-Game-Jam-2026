using Shears;
using UnityEngine;

namespace CultMask.Players
{
    [System.Serializable]
    public class PlayerDashState : PlayerState
    {
        public PlayerDashState()
        {
            Name = "Dash";
        }

        protected override void OnEnter()
        {
            ApplyHorizontalForce();
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
            AfterDashUpdateMovement();

            if (!Flags.CanStopDashing)
                ApplyVerticalAcceleration();
            else
                ApplyGravity();
        }

        private void ApplyHorizontalForce()
        {
            var inputDirection = GetInputDirection();

            if (inputDirection == Vector3.zero)
                inputDirection = Character.transform.forward;

            var horizontalForce = Data.DashHorizontalForce * inputDirection;
            Controller.SetVelocity(x: horizontalForce.x, z: horizontalForce.z);
            ClampHorizontalVelocity(Data.DashMaxSpeed);

            Character.transform.rotation = Quaternion.LookRotation(inputDirection, Vector3.up);
        }

        private void ApplyVerticalAcceleration()
        {
            var verticalAcceleration = Data.DashVerticalAcceleration * Time.deltaTime;

            if (Controller.Velocity.y < 0)
                verticalAcceleration *= Data.DashUpwardMultiplier;

            Controller.AddVelocity(new(0, verticalAcceleration, 0));
        }
    }
}
