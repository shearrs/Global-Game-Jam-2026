using Shears;
using UnityEngine;

namespace CultMask.Players
{
    [System.Serializable]
    public class PlayerStateFlags
    {
        [SerializeField, ReadOnly]
        private float moveInputMagnitude;

        [SerializeField, ReadOnly]
        private bool isGrounded;

        private readonly Player player;

        private PlayerInput Input => player.Input;
        private PlayerController Controller => player.Controller;

        public float MoveInputMagnitude => moveInputMagnitude;
        public bool IsGrounded => isGrounded;

        public PlayerStateFlags(Player player)
        {
            this.player = player;
        }

        public void Update()
        {
            moveInputMagnitude = Input.MoveInput.ReadValue<Vector2>().sqrMagnitude;
            isGrounded = Controller.IsGrounded;
        }
    }
}
