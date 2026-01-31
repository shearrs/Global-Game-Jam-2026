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

        private CharacterController Controller => TypedWrappedValue;

        public bool IsGrounded { get; private set; }

        private void Update()
        {
            UpdateIsGrounded();
        }

        public void Move(Vector3 movement)
        {
            Controller.Move(movement);
        }

        private void UpdateIsGrounded()
        {
            IsGrounded = groundDetector.Detect();
        }
    }
}
