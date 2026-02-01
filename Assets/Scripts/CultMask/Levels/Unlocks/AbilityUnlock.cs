using UnityEngine;

namespace CultMask.Levels
{
    public class AbilityUnlock : MonoBehaviour
    {
        public enum AbilityType { Vision, Dash, DoubleJump }

        [field: SerializeField]
        public AbilityType Type { get; private set; }

        public void Acquire()
        {
            Destroy(gameObject);
        }
    }
}
