using Shears;
using UnityEngine;

namespace CultMask.Players
{
    [CreateAssetMenu(menuName = "CultMask/Player/Data")]
    public class PlayerCharacterData : ScriptableObject
    {
        #region Grounded
        [FoldoutGroup("Grounded", 4)]
        [SerializeField]
        private float walkAcceleration = 50.0f;

        [SerializeField]
        private float walkDeceleration = 100.0f;

        [SerializeField]
        private float maxWalkSpeed = 12.0f;

        [SerializeField]
        private float rotationSpeed = 500.0f;
        #endregion

        #region Aerial
        [FoldoutGroup("Aerial", 5)]
        [SerializeField]
        private float jumpForce = 14.0f;

        [SerializeField]
        private float jumpBufferTime = 0.1f;

        [SerializeField]
        private float minimumJumpTime = 0.1f;

        [SerializeField]
        private float gravity = -30.0f;

        [SerializeField]
        private float fastFallGravity = -100f;
        #endregion

        #region Controlled
        [FoldoutGroup("Controlled", 2)]
        [SerializeField]
        private float characterHeight = 2.0f;

        [SerializeField]
        private float ledgeHangDistance = 0.5f;
        #endregion

        #region Dash
        [FoldoutGroup("Dash", 9)]
        [SerializeField]
        private float dashVerticalAcceleration = 4.0f;

        [SerializeField]
        private float dashUpwardMultiplier = 100.0f;

        [SerializeField]
        private float dashHorizontalForce = 14.0f;

        [SerializeField]
        private float dashControlAcceleration = 24.0f;

        [SerializeField]
        private float dashControlDeceleration = 2.0f;

        [SerializeField]
        private float dashMaxSpeed = 18.0f;

        [SerializeField]
        private float dashDuration = 0.25f;

        [SerializeField]
        private float dashJumpWindow = 0.05f;

        [SerializeField]
        private float dashJumpForce = 20.0f;
        #endregion

        #region Double Jump
        [FoldoutGroup("Double Jump", 1)]
        [SerializeField]
        private float doubleJumpForce = 12.0f;
        #endregion

        #region Vision
        [FoldoutGroup("Vision", 2)]
        [SerializeField]
        private float visionDuration = 3.0f;

        [SerializeField]
        private float visionCooldown = 3.0f;
        #endregion

        #region Properties
        public float WalkAcceleration => walkAcceleration;
        public float WalkDeceleration => walkDeceleration;
        public float MaxWalkSpeed => maxWalkSpeed;
        public float RotationSpeed => rotationSpeed;
        public float JumpForce => jumpForce;
        public float JumpBufferTime => jumpBufferTime;
        public float MinimumJumpTime => minimumJumpTime;
        public float Gravity => gravity;
        public float FastFallGravity => fastFallGravity;
        public float CharacterHeight => characterHeight;
        public float LedgeHangDistance => ledgeHangDistance;
        public float DashVerticalAcceleration => dashVerticalAcceleration;
        public float DashUpwardMultiplier => dashUpwardMultiplier;
        public float DashHorizontalForce => dashHorizontalForce;
        public float DashControlAcceleration => dashControlAcceleration;
        public float DashControlDeceleration => dashControlDeceleration;
        public float DashMaxSpeed => dashMaxSpeed;
        public float DashDuration => dashDuration;
        public float DashJumpWindow => dashJumpWindow;
        public float DashJumpForce => dashJumpForce;
        public float DoubleJumpForce => doubleJumpForce;
        public float VisionDuration => visionDuration;
        public float VisionCooldown => visionCooldown;
        #endregion
    }
}
