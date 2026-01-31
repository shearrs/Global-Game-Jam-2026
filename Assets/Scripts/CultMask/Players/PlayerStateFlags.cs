using Shears;
using Shears.StateMachineGraphs;
using UnityEngine;

namespace CultMask.Players
{
    [System.Serializable]
    public class PlayerStateFlags
    {
        [Header("Grounded")]
        [SerializeField, ReadOnly]
        private bool isGrounded;

        [SerializeField, ReadOnly]
        private float moveInputMagnitude;

        [SerializeField, ReadOnly]
        private bool isJumping;

        [SerializeField, ReadOnly]
        private Timer jumpBufferTimer = new();

        [Header("Ledges")]
        [SerializeField, ReadOnly]
        private bool isDetectingLedge;

        [SerializeField, ReadOnly]
        private Timer jumpGroundedTimer = new(0.1f);

        [SerializeField, ReadOnly]
        private Timer ledgeRegrabTimer = new(0.5f);

        [Header("Dashes")]
        [SerializeField, ReadOnly]
        private bool hasDashed = false;

        [SerializeField, ReadOnly]
        private Timer dashTimer = new();

        [SerializeField, ReadOnly]
        private Timer dashJumpWindowTimer = new();

        [SerializeField, ReadOnly]
        private Timer dashJumpInputCooldown = new(0.5f);

        private readonly PlayerCharacter player;
        private readonly PlayerLedgeDetector ledgeDetector;

        private PlayerInput Input => player.Input;
        private PlayerController Controller => player.Controller;

        public bool IsGrounded => isGrounded && jumpGroundedTimer.IsDone;
        public bool IsJumping => isJumping;
        public bool IsJumpBuffered => !jumpBufferTimer.IsDone;
        public bool IsDetectingLedge => isDetectingLedge && ledgeRegrabTimer.IsDone;
        public float MoveInputMagnitude => moveInputMagnitude;
        public bool HasDashed => hasDashed;
        public bool CanStopDashing => dashTimer.IsDone;
        public bool CanDashJump => dashJumpInputCooldown.IsDone && !dashJumpWindowTimer.IsDone;

        public PlayerStateFlags(PlayerCharacter player)
        {
            this.player = player;
            ledgeDetector = player.LedgeDetector;

            player.StateMachine.EnteredState += OnStateEntered;
            player.StateMachine.ExitedState += OnStateExited;
            Input.JumpInput.Performed += OnJumpInput;

            Application.quitting += Dispose;
        }

        ~PlayerStateFlags()
        {
            Dispose();
        }

        private void Dispose()
        {
            if (player == null)
                return;

            player.StateMachine.EnteredState -= OnStateEntered;
            player.StateMachine.ExitedState -= OnStateExited;
            Input.JumpInput.Performed -= OnJumpInput;
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
            else if (state is PlayerGroundedState)
            {
                if (hasDashed)
                    dashJumpWindowTimer.Restart(player.Data.DashJumpWindow);

                hasDashed = false;
            }
            else if (state is PlayerDashState)
            {
                hasDashed = true;
                dashTimer.Restart(player.Data.DashDuration);
            }
        }

        private void OnStateExited(State state)
        {
            if (state is PlayerJumpState)
                isJumping = false;
            else if (state is PlayerLedgeHangState)
                ledgeRegrabTimer.Restart();
        }

        private void OnJumpInput()
        {
            jumpBufferTimer.Restart(player.Data.JumpBufferTime);

            if (!player.StateMachine.IsInStateOfType<PlayerGroundedState>())
                dashJumpInputCooldown.Restart();
        }
    }
}
