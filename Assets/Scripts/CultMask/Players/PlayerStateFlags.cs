using CultMask.Levels;
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
        private bool isHanging = false;

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
        private bool hasDoubleJumped = false;
        #endregion

        private readonly PlayerCharacter character;
        private readonly PlayerLedgeDetector ledgeDetector;
        private readonly PlayerVisionManager visionManager;
        private readonly PlayerUnlocks unlocks;

        #region Properties
        private PlayerInput Input => character.Input;
        private PlayerController Controller => character.Controller;

        public bool DashUnlocked => unlocks.DashUnlocked;
        public bool DoubleJumpUnlocked => unlocks.DoubleJumpUnlocked;
        public bool VisionUnlocked => unlocks.VisionUnlocked;

        public bool IsGrounded => isGrounded && jumpGroundedTimer.IsDone;
        public bool IsJumping => isJumping;
        public bool IsJumpBuffered => !jumpBufferTimer.IsDone;
        public bool IsDetectingLedge => isDetectingLedge && ledgeRegrabTimer.IsDone;
        public bool IsHanging => isHanging;
        public float MoveInputMagnitude => moveInputMagnitude;
        public bool IsDashBuffered => Input.DashInput.WasPressedThisFrame();
        public bool HasDashed => hasDashed;
        public bool CanDash => DashUnlocked && !hasDashed;
        public bool CanStopDashing => dashTimer.IsDone;
        public bool CanDashJump => dashJumpInputCooldown.IsDone && !dashJumpWindowTimer.IsDone;
        public bool HasDoubleJumped => hasDoubleJumped;
        public bool CanDoubleJump => DoubleJumpUnlocked && !hasDoubleJumped;
        public bool CanUseVision => VisionUnlocked && !visionManager.IsVisionActive && !visionManager.IsVisionOnCooldown;
        public bool IsPunching => character.PunchManager.IsPunching;
        public bool CanPunch => character.PunchManager.CanPunch;
        #endregion

        public PlayerStateFlags(PlayerCharacter character)
        {
            this.character = character;
            unlocks = character.Player.Unlocks;
            ledgeDetector = character.LedgeDetector;
            visionManager = character.VisionManager;

            character.StateMachine.EnteredState += OnStateEntered;
            character.StateMachine.ExitedState += OnStateExited;
            Input.JumpInput.Performed += OnJumpInput;

            Application.quitting += Dispose;
        }

        ~PlayerStateFlags()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (character == null)
                return;

            character.StateMachine.EnteredState -= OnStateEntered;
            character.StateMachine.ExitedState -= OnStateExited;
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
            else if (state is PlayerDoubleJumpState)
                isJumping = true;
            else if (state is PlayerGroundedState)
            {
                if (!dashEndedTimer.IsDone)
                    dashJumpWindowTimer.Restart(character.Data.DashJumpWindow);

                hasDashed = false;
                hasDoubleJumped = false;
            }
            else if (state is PlayerDashState)
            {
                hasDashed = true;
                dashTimer.Restart(character.Data.DashDuration);
            }
            else if (state is PlayerDoubleJumpState)
            {
                jumpBufferTimer.Stop();
                hasDoubleJumped = true;
            }
            else if (state is PlayerLedgeHangState)
                isHanging = true;
        }

        private void OnStateExited(State state)
        {
            if (state is PlayerJumpState)
                isJumping = false;
            else if (state is PlayerDoubleJumpState)
                isJumping = false;
            else if (state is PlayerLedgeHangState)
            {
                isHanging = false;
                ledgeRegrabTimer.Restart();
            }
            else if (state is PlayerDashState)
                dashEndedTimer.Restart();
        }

        private void OnJumpInput()
        {
            jumpBufferTimer.Restart(character.Data.JumpBufferTime);

            if (!character.StateMachine.IsInStateOfType<PlayerGroundedState>())
                dashJumpInputCooldown.Restart();
        }
    }
}
