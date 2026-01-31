using Shears;
using Shears.StateMachineGraphs;
using System;
using UnityEngine;

namespace CultMask.Players
{
    [System.Serializable]
    public abstract class PlayerState : State
    {
        private Vector3 lastRotationDirection;

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
            var inputDirection = (forward + right).normalized;
            var horizontalVelocity = Controller.Velocity.With(y: 0);
            var currentDirection = horizontalVelocity.normalized;
            float inputAlignment = Vector3.Dot(currentDirection, inputDirection);

            var acceleration = Time.deltaTime * Data.WalkAcceleration * inputDirection;
            var deceleration = Time.deltaTime * Data.WalkDeceleration * (1 - inputAlignment) * -currentDirection;

            var movement = horizontalVelocity + acceleration;

            Vector3 firstSigns = new(Math.Sign(movement.x), 0, Math.Sign(movement.z));

            movement += deceleration;

            Vector3 secondSigns = new(Math.Sign(movement.x), 0, Math.Sign(movement.z));

            if (firstSigns.x != secondSigns.x)
            {
                movement.x = 0.0f;
                Controller.SetVelocityX(0.0f);
            }
            if (firstSigns.z != secondSigns.z)
            {
                movement.z = 0.0f;
                Controller.SetVelocityZ(0.0f);
            }

            if (movement.sqrMagnitude > Data.MaxWalkSpeed * Data.MaxWalkSpeed)
                movement = movement.normalized * Data.MaxWalkSpeed;

            if (Flags.IsGrounded)
                Controller.SetVelocityY(0.0f);

            Controller.Move(Time.deltaTime * movement);

            var rotationDirection = movement != Vector3.zero ? movement : lastRotationDirection;

            if (rotationDirection != Vector3.zero)
            {
                Controller.RotateToDirection(rotationDirection, Data.RotationSpeed);
                lastRotationDirection = rotationDirection;
            }
        }
    }
}
