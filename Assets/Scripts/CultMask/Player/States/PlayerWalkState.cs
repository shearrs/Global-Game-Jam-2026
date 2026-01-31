using UnityEngine;

namespace CultMask.Players
{
    public class PlayerWalkState : PlayerState
    {
        protected override void OnEnter()
        {
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
            UpdateMovement();
        }

        private void UpdateMovement()
        {
            var moveInput = Input.MoveInput.ReadValue<Vector2>();

            var forward = (Time.deltaTime * moveInput.y * Camera.transform.forward).normalized;
            var right = (Time.deltaTime * moveInput.x * Camera.transform.right).normalized;

            var movement = Data.WalkSpeed * (forward + right);

            Controller.Move(movement);
        }
    }
}
