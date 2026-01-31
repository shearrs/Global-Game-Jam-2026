using Shears;
using Shears.StateMachineGraphs;
using UnityEngine;

namespace CultMask.Players
{
    [System.Serializable]
    public class PlayerStateFlags
    {
        [SerializeField, ReadOnly]
        private bool isGrounded;

        [SerializeField, ReadOnly]
        private bool isJumping;

        [SerializeField, ReadOnly]
        private bool isDetectingLedge;

        [SerializeField, ReadOnly]
        private float moveInputMagnitude;

        [SerializeField, ReadOnly]
        private Timer jumpGroundedTimer = new(0.1f);

        [SerializeField, ReadOnly]
        private Timer ledgeRegrabTimer = new(0.5f);

        private readonly PlayerCharacter player;
        private readonly PlayerLedgeDetector ledgeDetector;

        private PlayerInput Input => player.Input;
        private PlayerController Controller => player.Controller;

        public bool IsGrounded => isGrounded && jumpGroundedTimer.IsDone;
        public bool IsJumping => isJumping;
        public bool IsDetectingLedge => isDetectingLedge && ledgeRegrabTimer.IsDone;
        public float MoveInputMagnitude => moveInputMagnitude;

        public PlayerStateFlags(PlayerCharacter player)
        {
            this.player = player;
            ledgeDetector = player.LedgeDetector;

            player.StateMachine.EnteredState += OnStateEntered;
            player.StateMachine.ExitedState += OnStateExited;
        }

        ~PlayerStateFlags()
        {
            player.StateMachine.EnteredState -= OnStateEntered;
            player.StateMachine.ExitedState -= OnStateExited;
        }

        public void Update()
        {
            moveInputMagnitude = Input.MoveInput.ReadValue<Vector2>().sqrMagnitude;
            isGrounded = Controller.IsGrounded;
            isDetectingLedge = ledgeDetector.IsLedgeDetected;
        }

        private void OnStateEntered(State state)
        {
            if (state is PlayerJumpState)
            {
                jumpGroundedTimer.Restart();
                isJumping = true;
            }
        }

        private void OnStateExited(State state)
        {
            if (state is PlayerJumpState)
                isJumping = false;
            else if (state is PlayerLedgeHangState)
                ledgeRegrabTimer.Restart();
        }
    }
}
