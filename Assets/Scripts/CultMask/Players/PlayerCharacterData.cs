using UnityEngine;

namespace CultMask.Players
{
    [CreateAssetMenu(menuName = "CultMask/Player/Data")]
    public class PlayerCharacterData : ScriptableObject
    {
        [Header("Grounded")]
        [SerializeField]
        private float walkAcceleration = 40.0f;

        [SerializeField]
        private float walkDeceleration = 80.0f;

        [SerializeField]
        private float maxWalkSpeed = 12.0f;

        [SerializeField]
        private float rotationSpeed = 720.0f;

        [Header("Aerial")]
        [SerializeField]
        private float jumpForce = 14f;

        [SerializeField]
        private float gravity = -30f;

        [SerializeField]
        private float fastFallGravity = -50f;

        [Header("Controlled")]
        [SerializeField]
        private float characterHeight = 2.0f;

        [SerializeField]
        private float ledgeHangDistance = 0.5f;

        public float WalkAcceleration => walkAcceleration;
        public float WalkDeceleration => walkDeceleration;
        public float MaxWalkSpeed => maxWalkSpeed;
        public float RotationSpeed => rotationSpeed;
        public float JumpForce => jumpForce;
        public float Gravity => gravity;
        public float FastFallGravity => fastFallGravity;
        public float CharacterHeight => characterHeight;
        public float LedgeHangDistance => ledgeHangDistance;
    }
}
