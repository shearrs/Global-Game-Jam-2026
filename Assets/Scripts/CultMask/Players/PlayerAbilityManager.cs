using CultMask.Levels;
using Shears.Detection;
using System;
using UnityEngine;

namespace CultMask.Players
{
    public class PlayerAbilityManager : MonoBehaviour
    {
        [SerializeField]
        private AreaDetector3D detector;

        private PlayerStateFlags flags;

        public event Action<AbilityUnlock> AbilityUnlocked;

        public void Initialize(PlayerCharacter character)
        {
            flags = character.StateFlags;
        }

        private void Update()
        {
            if (flags == null)
                return;

            DetectUnlock();
        }

        private void DetectUnlock()
        {
            if (!detector.Detect())
                return;

            if (!detector.TryGetDetection(out AbilityUnlock unlock))
                return;

            flags.UnlockAbility(unlock.Data);
            AbilityUnlocked?.Invoke(unlock);
            unlock.Acquire();
        }
    }
}
