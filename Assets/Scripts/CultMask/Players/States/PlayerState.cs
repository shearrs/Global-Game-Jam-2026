using Shears;
using Shears.StateMachineGraphs;
using System;
using UnityEngine;

namespace CultMask.Players
{
    [Serializable]
    public abstract class PlayerState : State
    {
        protected Player Player { get; private set; }
        protected PlayerInput Input => Player.Input;
        protected PlayerCharacter Character => Player.Character;
        protected PlayerCharacterData Data => Character.Data;
        protected PlayerCamera Camera => Character.Camera;
        protected PlayerController Controller => Character.Controller;
        protected PlayerStateFlags Flags => Character.StateFlags;

        public void Initialize(Player player)
        {
            Player = player;
        }

        public void AdaptiveUpdateMovement(float? moveAcceleration = null, float? moveDeceleration = null, float? maxSpeed = null, float? rotationSpeed = null)
        {
            if (Flags.HasDashed)
                AfterDashUpdateMovement();
            else
                StandardUpdateMovement(moveAcceleration, moveDeceleration, maxSpeed, rotationSpeed);
        }

        public void AfterDashUpdateMovement() => StandardUpdateMovement(moveAcceleration: Data.DashControlAcceleration, moveDeceleration: Data.DashControlDeceleration, maxSpeed: Data.DashMaxSpeed, rotationSpeed: 0.35f * Data.RotationSpeed);

        public void StandardUpdateMovement(float? moveAcceleration = null, float? moveDeceleration = null, float? maxSpeed = null, float? rotationSpeed = null)
        {
            float resolvedAcceleration = moveAcceleration ?? Data.WalkAcceleration;
            float resolvedDeceleration = moveDeceleration ?? Data.WalkDeceleration;
            float resolvedMaxSpeed = maxSpeed ?? Data.MaxWalkSpeed;

            var inputDirection = GetInputDirection();
            var currentDirection = Controller.Velocity.With(y: 0).normalized;
            float inputAlignment = Vector3.Dot(currentDirection, inputDirection);

            var acceleration = Time.deltaTime * resolvedAcceleration * inputDirection;
            var deceleration = Time.deltaTime * resolvedDeceleration * (1 - inputAlignment) * -currentDirection;
            var movement = acceleration + deceleration;

            Vector3 firstSigns = new(Math.Sign(Controller.Velocity.x), 0, Math.Sign(Controller.Velocity.z));
            Vector3 secondSigns = new(Math.Sign(Controller.Velocity.x + movement.x), 0, Math.Sign(Controller.Velocity.z + movement.z));

            if (firstSigns.x != secondSigns.x && firstSigns.x != 0 && secondSigns.x != 0)
            {
                movement.x = 0.0f;
                Controller.SetVelocity(x: 0.0f);
            }
            if (firstSigns.z != secondSigns.z && firstSigns.z != 0 && secondSigns.z != 0)
            {
                movement.z = 0.0f;
                Controller.SetVelocity(z: 0.0f);
            }

            Controller.AddVelocity(movement);

            var horizontalVelocity = Controller.Velocity.With(y: 0.0f);

            if (horizontalVelocity != Vector3.zero)
            {
                float resolvedRotationSpeed = rotationSpeed ?? Data.RotationSpeed;

                if (Flags.IsPunchWindingDown)
                    resolvedRotationSpeed = 1.0f;

                Controller.RotateToDirection(horizontalVelocity, resolvedRotationSpeed);
            }

            ClampHorizontalVelocity(resolvedMaxSpeed);
        }

        public void ClampHorizontalVelocity(float maxSpeed)
        {
            var horizontalVelocity = Controller.Velocity.With(y: 0);

            if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
            {
                horizontalVelocity = horizontalVelocity.normalized * maxSpeed;
                Controller.SetVelocity(x: horizontalVelocity.x, z: horizontalVelocity.z);
            }
        }

        public Vector3 GetInputDirection()
        {
            var moveInput = Input.MoveInput.ReadValue<Vector2>();

            var cameraForward = Camera.transform.forward.With(y: 0).normalized;
            var cameraRight = Camera.transform.right.With(y: 0).normalized;

            var forward = (moveInput.y * cameraForward);
            var right = (moveInput.x * cameraRight);
            var inputDirection = forward + right;

            if (inputDirection.sqrMagnitude > 1.0f)
                inputDirection.Normalize();

            return inputDirection;
        }

        public void AdaptiveApplyGravity()
        {
            if (!Controller.IsGrounded)
                ApplyGravity();
        }

        public void ApplyGravity()
        {
            Controller.AddVelocity(Data.Gravity * Time.deltaTime * Vector3.up);
        }
    }
}
