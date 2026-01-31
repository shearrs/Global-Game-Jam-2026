using Shears;
using UnityEngine;

namespace CultMask.Players
{
    [CreateAssetMenu(menuName = "CultMask/Player/Data")]
    public class PlayerCharacterData : ScriptableObject
    {
        [FoldoutGroup("Grounded", 4)]
        [SerializeField]
        private float walkAcceleration = 50.0f;

        [SerializeField]
        private float walkDeceleration = 100.0f;

        [SerializeField]
        private float maxWalkSpeed = 12.0f;

        [SerializeField]
        private float rotationSpeed = 500.0f;

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

        [FoldoutGroup("Controlled", 2)]
        [SerializeField]
        private float characterHeight = 2.0f;

        [SerializeField]
        private float ledgeHangDistance = 0.5f;

        [FoldoutGroup("Dash", 9)]
        [SerializeField]
        private float dashVerticalAcceleration = 10.0f;

        [SerializeField]
        private float dashUpwardMultiplier = 2.0f;

        [SerializeField]
        private float dashHorizontalForce = 20.0f;

        [SerializeField]
        private float dashControlAcceleration = 8.0f;

        [SerializeField]
        private float dashControlDeceleration = 2.0f;

        [SerializeField]
        private float dashMaxSpeed = 20.0f;

        [SerializeField]
        private float dashDuration = 0.5f;

        [SerializeField]
        private float dashJumpWindow = 0.15f;

        [SerializeField]
        private float dashJumpForce = 28.0f;

        [FoldoutGroup("Double Jump", 1)]
        [SerializeField]
        private float doubleJumpForce = 8.0f;

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
    }
}
