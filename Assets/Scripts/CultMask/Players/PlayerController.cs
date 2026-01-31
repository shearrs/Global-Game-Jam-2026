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

        private void LateUpdate()
        {
            Controller.Move(velocity * Time.deltaTime);
            velocity = Controller.velocity;
        }

        public void SetVelocity(Vector3 movement)
        {
            velocity = movement;
        }

        public void SetVelocity(float? x = null, float? y = null, float? z = null)
        {
            if (x.HasValue)
                velocity.x = x.Value;
            if (y.HasValue)
                velocity.y = y.Value;
            if (z.HasValue)
                velocity.z = z.Value;
        }

        public void AddVelocity(Vector3 movement)
        {
            velocity += movement;
        }

        public void RotateToDirection(Vector3 direction, float rotationSpeed)
        {
            var lookRotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }

        private void UpdateIsGrounded()
        {
            IsGrounded = groundDetector.Detect();
        }
    }
}
