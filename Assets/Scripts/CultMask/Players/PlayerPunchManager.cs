using Shears;
using Shears.HitDetection;
using System;
using UnityEngine;

namespace CultMask.Players
{
    public partial class PlayerPunchManager : MonoBehaviour
    {
        [SerializeField]
        [AutoEvent(nameof(HitBody3D.HitDelivered), nameof(OnHitDelivered))]
        private HitBody3D hitBody;

        private readonly Timer punchActiveTimer = new();
        private readonly Timer punchCooldownTimer = new();
        private PlayerCharacterData data;

        public bool IsPunching => !punchActiveTimer.IsDone;
        public bool IsPunchWindingDown => !punchCooldownTimer.IsDone;
        public bool CanPunch => punchActiveTimer.IsDone && punchCooldownTimer.IsDone;

        public event Action<HitData3D> HitDelivered;

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

        private void OnHitDelivered(HitData3D hit)
        {
            HitDelivered?.Invoke(hit);
        }
    }
}
