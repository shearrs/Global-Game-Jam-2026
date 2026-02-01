using Shears;
using Shears.HitDetection;
using UnityEngine;

namespace CultMask.Players
{
    public class PlayerPunchManager : MonoBehaviour
    {
        [SerializeField]
        private HitBody3D hitBody;

        private readonly Timer punchActiveTimer = new();
        private readonly Timer punchCooldownTimer = new();
        private PlayerCharacterData data;

        public bool IsPunching => !punchActiveTimer.IsDone;
        public bool CanPunch => punchActiveTimer.IsDone && punchCooldownTimer.IsDone;

        private void Awake()
        {
            punchActiveTimer.Completed += OnPunchTimerCompleted;
        }

        public void Initialize(PlayerCharacter character)
        {
            data = character.Data;
        }

        public void Punch()
        {
            hitBody.Enable();
            punchActiveTimer.Restart(data.PunchDuration);
        }

        private void OnPunchTimerCompleted()
        {
            hitBody.Disable();
            punchCooldownTimer.Start(data.PunchCooldown);
        }
    }
}
