using Shears;
using Shears.StateMachineGraphs;
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

        [SerializeField, ReadOnly]
        private Timer jumpGroundedTimer = new(0.1f);

        private readonly PlayerCharacter player;

        private PlayerInput Input => player.Input;
        private PlayerController Controller => player.Controller;

        public float MoveInputMagnitude => moveInputMagnitude;
        public bool IsGrounded => isGrounded && jumpGroundedTimer.IsDone;

        public PlayerStateFlags(PlayerCharacter player)
        {
            this.player = player;

            player.StateMachine.EnteredState += OnStateEntered;
        }

        ~PlayerStateFlags()
        {
            player.StateMachine.EnteredState -= OnStateEntered;
        }

        public void Update()
        {
            moveInputMagnitude = Input.MoveInput.ReadValue<Vector2>().sqrMagnitude;
            isGrounded = Controller.IsGrounded;
        }

        private void OnStateEntered(State state)
        {
            if (state is PlayerJumpState)
                jumpGroundedTimer.Restart();
        }
    }
}
