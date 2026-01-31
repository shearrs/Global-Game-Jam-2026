using UnityEngine;

namespace CultMask.Players
{
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private float walkSpeed = 6f;
        [SerializeField] private float jumpForce = 6f;

        public float WalkSpeed => walkSpeed;
        public float JumpForce => jumpForce;
    }
}
