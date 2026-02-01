using Shears;
using UnityEngine;

namespace CultMask.Players
{
    [CreateAssetMenu(menuName = "CultMask/Player/Data")]
    public class PlayerCharacterData : ScriptableObject
    {
        #region Grounded
        [field: FoldoutGroup("Grounded", 4)]
        [field: SerializeField]
        public float WalkAcceleration { get; private set; } = 50.0f;

        [field: SerializeField]
        public float WalkDeceleration { get; private set; } = 100.0f;

        [field: SerializeField]
        public float MaxWalkSpeed { get; private set; } = 12.0f;

        [field: SerializeField]
        public float RotationSpeed { get; private set; } = 500.0f;
        #endregion

        #region Aerial
        [field: FoldoutGroup("Aerial", 5)]
        [field: SerializeField]
        public float JumpForce { get; private set; } = 14.0f;

        [field: SerializeField]
        public float JumpBufferTime { get; private set; } = 0.1f;

        [field: SerializeField]
        public float MinimumJumpTime { get; private set; } = 0.1f;

        [field: SerializeField]
        public float Gravity { get; private set; } = -30.0f;

        [field: SerializeField]
        public float FastFallGravity { get; private set; } = -100f;
        #endregion

        #region Controlled
        [field: FoldoutGroup("Controlled", 2)]
        [field: SerializeField]
        public float CharacterHeight { get; private set; } = 2.0f;

        [field: SerializeField]
        public float LedgeHangDistance { get; private set; } = 0.5f;
        #endregion

        #region Dash
        [field: FoldoutGroup("Dash", 9)]
        [field: SerializeField]
        public float DashVerticalAcceleration { get; private set; } = 4.0f;

        [field: SerializeField]
        public float DashUpwardMultiplier { get; private set; } = 100.0f;

        [field: SerializeField]
        public float DashHorizontalForce { get; private set; } = 14.0f;

        [field: SerializeField]
        public float DashControlAcceleration { get; private set; } = 24.0f;

        [field: SerializeField]
        public float DashControlDeceleration { get; private set; } = 2.0f;

        [field: SerializeField]
        public float DashMaxSpeed { get; private set; } = 18.0f;

        [field: SerializeField]
        public float DashDuration { get; private set; } = 0.25f;

        [field: SerializeField]
        public float DashJumpWindow { get; private set; } = 0.05f;

        [field: SerializeField]
        public float DashJumpForce { get; private set; } = 20.0f;
        #endregion

        #region Double Jump
        [field: FoldoutGroup("Double Jump", 1)]
        [field: SerializeField]
        public float DoubleJumpForce { get; private set; } = 12.0f;
        #endregion

        #region Vision
        [field: FoldoutGroup("Vision", 2)]
        [field: SerializeField]
        public float VisionDuration { get; private set; } = 3.0f;

        [field: SerializeField]
        public float VisionCooldown { get; private set; } = 3.0f;
        #endregion

        #region Punch
        [field: FoldoutGroup("Punch", 2)]
        [field: SerializeField]
        public float PunchDuration { get; private set; } = 0.15f;

        [field: SerializeField]
        public float PunchCooldown { get; private set; } = 0.15f;
        #endregion

        #region Health
        [field: FoldoutGroup("Health", 1)]
        [field: SerializeField]
        public int MaxHealth { get; private set; } = 4;
        #endregion
    }
}
