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

        public void StandardUpdateMovement()
        {
            var moveInput = Input.MoveInput.ReadValue<Vector2>();

            var cameraForward = Camera.transform.forward.With(y: 0).normalized;
            var cameraRight = Camera.transform.right.With(y: 0).normalized;

            var forward = (moveInput.y * cameraForward);
            var right = (moveInput.x * cameraRight);
            var inputDirection = forward + right;
            var currentDirection = Controller.Velocity.With(y: 0).normalized;
            float inputAlignment = Vector3.Dot(currentDirection, inputDirection);

            var acceleration = Time.deltaTime * Data.WalkAcceleration * inputDirection;
            var deceleration = Time.deltaTime * Data.WalkDeceleration * (1 - inputAlignment) * -currentDirection;
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

            if (Flags.IsGrounded)
                Controller.SetVelocity(y: 0.0f);

            var rotationDirection = Controller.Velocity.With(y: 0) + acceleration;

            if (rotationDirection != Vector3.zero)
                Controller.RotateToDirection(rotationDirection, Data.RotationSpeed);

            Controller.AddVelocity(movement);

            var horizontalVelocity = Controller.Velocity.With(y: 0);

            if (horizontalVelocity.sqrMagnitude > Data.MaxWalkSpeed * Data.MaxWalkSpeed)
            {
                horizontalVelocity = horizontalVelocity.normalized * Data.MaxWalkSpeed;
                Controller.SetVelocity(x: horizontalVelocity.x, z: horizontalVelocity.z);
            }
        }
    }
}
