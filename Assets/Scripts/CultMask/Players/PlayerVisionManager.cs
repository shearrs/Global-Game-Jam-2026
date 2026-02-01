using CultMask.Levels;
using Shears;
using Shears.Logging;
using System;
using UnityEngine;

namespace CultMask.Players
{
    public class PlayerVisionManager : MonoBehaviour
    {
        [SerializeField, ReadOnly]
        private Timer visionActiveTimer = new();

        [SerializeField, ReadOnly]
        private Timer visionCooldownTimer = new();

        private PlayerInput input;
        private PlayerStateFlags stateFlags;
        private PlayerCharacterData data;

        public bool IsVisionActive => !visionActiveTimer.IsDone;
        public bool IsVisionOnCooldown => !visionCooldownTimer.IsDone;

        public event Action VisionActivated;
        public event Action VisionDeactivated;

        private void OnDestroy()
        {
            if (input != null)
                input.ActivateVisionInput.Performed -= OnActivateVisionInput;
        }

        public void Initialize(PlayerCharacter character)
        {
            if (input != null)
            {
                SHLogger.Log($"{nameof(PlayerVisionManager)} is already initialized!", SHLogLevels.Error);
                return;
            }

            input = character.Input;
            stateFlags = character.StateFlags;
            data = character.Data;
            input.ActivateVisionInput.Performed += OnActivateVisionInput;

            visionActiveTimer.Completed += DeactivateVision;
        }

        private void OnActivateVisionInput()
        {
            if (stateFlags.CanUseVision)
                ActivateVision();
        }

        private void ActivateVision()
        {
            VisionElementManager.EnableVision();

            visionActiveTimer.Restart(data.VisionDuration);

            VisionActivated?.Invoke();
        }

        private void DeactivateVision()
        {
            VisionElementManager.DisableVision();

            visionCooldownTimer.Restart(data.VisionCooldown);

            VisionDeactivated?.Invoke();
        }
    }
}
