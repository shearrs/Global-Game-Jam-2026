using UnityEngine;

namespace CultMask.Levels
{
    public class AbilityUnlock : MonoBehaviour
    {
        [SerializeField]
        private AbilityData data;

        public AbilityData Data => data;

        public void Acquire()
        {
            Destroy(gameObject);
        }
    }
}
