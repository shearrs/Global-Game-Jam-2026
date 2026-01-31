using Shears;
using Shears.StateMachineGraphs;
using UnityEngine;

namespace CultMask.Players
{
    [System.Serializable]
    public class PlayerStateFlags
    {
        #region Grounded
        [Header("Grounded")]
        [SerializeField, ReadOnly]
        private bool isGrounded = false;

        [SerializeField, ReadOnly]
        private float moveInputMagnitude;

        [SerializeField, ReadOnly]
        private bool isJumping = false;

        [SerializeField, ReadOnly]
        private Timer jumpBufferTimer = new();
        #endregion

        #region Ledge Hang
        [Header("Ledge Hang")]
        [SerializeField, ReadOnly]
        private bool isDetectingLedge = false;

        [SerializeField, ReadOnly]
        private Timer jumpGroundedTimer = new(0.1f);

        [SerializeField, ReadOnly]
        private Timer ledgeRegrabTimer = new(0.5f);
        #endregion

        #region Dash
        [Header("Dash")]
        [SerializeField, ReadOnly]
        private bool hasDashed = false;

        [SerializeField, ReadOnly]
        private Timer dashTimer = new();

        [SerializeField, ReadOnly]
        private Timer dashEndedTimer = new(0.5f);
        
        [SerializeField, ReadOnly]
        private Timer dashJumpWindowTimer = new();

        [SerializeField, ReadOnly]
        private Timer dashJumpInputCooldown = new(0.5f);
        #endregion

        #region Double Jump
        [Header("Double Jump")]
        [SerializeField, ReadOnly]
        private bool doubleJumpUnlocked = true;

        [SerializeField, ReadOnly]
        private bool hasDoubleJumped = false;
        #endregion

        #region Vision
        [Header("Vision")]
        [SerializeField, ReadOnly]
        private bool visionUnlocked = true;
        #endregion

        private readonly PlayerCharacter player;
        private readonly PlayerLedgeDetector ledgeDetector;
        private readonly PlayerVisionManager visionManager;

        #region Properties
        private PlayerInput Input => player.Input;
        private PlayerController Controller => player.Controller;

        public bool IsGrounded => isGrounded && jumpGroundedTimer.IsDone;
        public bool IsJumping => isJumping;
        public bool IsJumpBuffered => !jumpBufferTimer.IsDone;
        public bool IsDetectingLedge => isDetectingLedge && ledgeRegrabTimer.IsDone;
        public float MoveInputMagnitude => moveInputMagnitude;
        public bool IsDashBuffered => Input.DashInput.WasPressedThisFrame();
        public bool HasDashed => hasDashed;
        public bool CanStopDashing => dashTimer.IsDone;
        public bool CanDashJump => dashJumpInputCooldown.IsDone && !dashJumpWindowTimer.IsDone;
        public bool DoubleJumpUnlocked => doubleJumpUnlocked;
        public bool HasDoubleJumped => hasDoubleJumped;
        public bool VisionUnlocked => visionUnlocked;
        public bool CanUseVision => visionUnlocked && !visionManager.IsVisionActive && !visionManager.IsVisionOnCooldown;
        #endregion

        public PlayerStateFlags(PlayerCharacter player)
        {
            this.player = player;
            ledgeDetector = player.LedgeDetector;
            visionManager = player.VisionManager;

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
                jumpBufferTimer.Stop();
                jumpGroundedTimer.Restart();
                isJumping = true;
            }
            else if (state is PlayerGroundedState)
            {
                if (!dashEndedTimer.IsDone)
                    dashJumpWindowTimer.Restart(player.Data.DashJumpWindow);

                hasDashed = false;
                hasDoubleJumped = false;
            }
            else if (state is PlayerDashState)
            {
                hasDashed = true;
                dashTimer.Restart(player.Data.DashDuration);
            }
            else if (state is PlayerDoubleJumpState)
            {
                jumpBufferTimer.Stop();
                hasDoubleJumped = true;
            }
        }

        private void OnStateExited(State state)
        {
            if (state is PlayerJumpState)
                isJumping = false;
            else if (state is PlayerLedgeHangState)
                ledgeRegrabTimer.Restart();
            else if (state is PlayerDashState)
                dashEndedTimer.Restart();
        }

        private void OnJumpInput()
        {
            jumpBufferTimer.Restart(player.Data.JumpBufferTime);

            if (!player.StateMachine.IsInStateOfType<PlayerGroundedState>())
                dashJumpInputCooldown.Restart();
        }
    }
}
