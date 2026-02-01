using UnityEngine;

namespace CultMask.Enemies
{
    [CreateAssetMenu(menuName = "CultMask/Enemies/Data")]
    public class EnemyData : ScriptableObject
    {
        [field: SerializeField]
        public float TargetDistance { get; private set; } = 3.0f;

        [field: SerializeField]
        public float AttackDelay { get; private set; } = 0.5f;

        [field: SerializeField]
        public float AttackDuration { get; private set; } = 0.15f;
    }
}
