using CultMask.Levels;
using Shears;
using UnityEngine;

namespace CultMask.Players
{
    [System.Serializable]
    public class PlayerUnlocks
    {
        [SerializeField, ReadOnly]
        private bool dashUnlocked = false;

        [SerializeField, ReadOnly]
        private bool doubleJumpUnlocked = false;

        [SerializeField, ReadOnly]
        private bool visionUnlocked = false;

        public bool DashUnlocked => dashUnlocked;
        public bool DoubleJumpUnlocked => doubleJumpUnlocked;
        public bool VisionUnlocked => visionUnlocked;

        public void UnlockAbility(AbilityData data)
        {
            switch (data.Type)
            {
                case AbilityData.AbilityType.Dash:
                    dashUnlocked = true;
                    break;
                case AbilityData.AbilityType.Vision:
                    visionUnlocked = true;
                    break;
                case AbilityData.AbilityType.DoubleJump:
                    doubleJumpUnlocked = true;
                    break;
            }
        }
    }
}
