using UnityEngine;

namespace CultMask.Players
{
    [CreateAssetMenu(menuName = "CultMask/Player/Data")]
    public class PlayerCharacterData : ScriptableObject
    {
        [Header("Grounded")]
        [SerializeField] private float walkAcceleration = 16.0f;
        [SerializeField] private float walkDeceleration = 10.0f;
        [SerializeField] private float maxWalkSpeed = 5.0f;
        [SerializeField] private float rotationSpeed = 360.0f;

        [Header("Aerial")]
        [SerializeField] private float jumpForce = 14f;
        [SerializeField] private float gravity = -30f;

        public float WalkAcceleration => walkAcceleration;
        public float WalkDeceleration => walkDeceleration;
        public float MaxWalkSpeed => maxWalkSpeed;
        public float RotationSpeed => rotationSpeed;
        public float JumpForce => jumpForce;
        public float Gravity => gravity;
    }
}
