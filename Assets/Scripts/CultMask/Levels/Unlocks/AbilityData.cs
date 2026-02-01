using UnityEngine;

namespace CultMask.Levels
{
    [CreateAssetMenu(menuName = "CultMask/Ability Data")]
    public class AbilityData : ScriptableObject
    {
        public enum AbilityType { Vision, Dash, DoubleJump };

        [SerializeField]
        private AbilityType type;

        [SerializeField]
        private string title;

        [SerializeField]
        private string description;

        public AbilityType Type => type;
        public string Title => title;
        public string Description => description;
    }
}
