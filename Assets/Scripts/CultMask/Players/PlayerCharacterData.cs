using UnityEngine;

namespace CultMask.Players
{
    [CreateAssetMenu(menuName = "CultMask/Player/Data")]
    public class PlayerCharacterData : ScriptableObject
    {
        [Header("Grounded")]
        [SerializeField]
        private float walkAcceleration = 50.0f;

        [SerializeField]
        private float walkDeceleration = 100.0f;

        [SerializeField]
        private float maxWalkSpeed = 12.0f;

        [SerializeField]
        private float rotationSpeed = 500.0f;

        [Header("Aerial")]
        [SerializeField]
        private float jumpForce = 14f;

        [SerializeField]
        private float gravity = -30f;

        [SerializeField]
        private float fastFallGravity = -100f;

        [Header("Controlled")]
        [SerializeField]
        private float characterHeight = 2.0f;

        [SerializeField]
        private float ledgeHangDistance = 0.5f;

        [Header("Dash")]
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

        public float WalkAcceleration => walkAcceleration;
        public float WalkDeceleration => walkDeceleration;
        public float MaxWalkSpeed => maxWalkSpeed;
        public float RotationSpeed => rotationSpeed;
        public float JumpForce => jumpForce;
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
    }
}
