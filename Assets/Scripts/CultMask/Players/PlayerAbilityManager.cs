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

        private Player player;

        public event Action<AbilityUnlock> AbilityUnlocked;

        public void Initialize(PlayerCharacter character)
        {
            player = character.Player;
        }

        private void Update()
        {
            if (player == null)
                return;

            DetectUnlock();
        }

        private void DetectUnlock()
        {
            if (!detector.Detect())
                return;

            if (!detector.TryGetDetection(out AbilityUnlock unlock))
                return;

            player.UnlockAbility(unlock.Data);
            AbilityUnlocked?.Invoke(unlock);
            unlock.Acquire();
        }
    }
}
