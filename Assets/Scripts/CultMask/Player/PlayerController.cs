using Shears;
using Shears.Detection;
using UnityEngine;

namespace CultMask.Players
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : ManagedWrapper<CharacterController>
    {
        [SerializeField]
        private AreaDetector3D groundDetector;

        private Vector3 velocity;

        private CharacterController Controller => TypedWrappedValue;

        public bool IsGrounded { get; private set; }
        public Vector3 Velocity => velocity;
        public Quaternion Rotation => transform.rotation;

        private void Update()
        {
            UpdateIsGrounded();
        }

        public void Move(Vector3 movement)
        {
            Controller.Move(movement);

            if (Mathf.Abs(movement.x) > 0.0f)
                velocity.x = Controller.velocity.x;
            if (Mathf.Abs(movement.y) > 0.0f)
                velocity.y = Controller.velocity.y;
            if (Mathf.Abs(movement.z) > 0.0f)
                velocity.z = Controller.velocity.z;
        }

        public void RotateToDirection(Vector3 direction, float rotationSpeed)
        {
            var lookRotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }

        public void SetVelocityX(float newX)
        {
            velocity.x = newX;
        }

        public void SetVelocityY(float newY)
        {
            velocity.y = newY;
        }

        public void SetVelocityZ(float newZ)
        {
            velocity.z = newZ;
        }

        private void UpdateIsGrounded()
        {
            IsGrounded = groundDetector.Detect();
        }
    }
}
