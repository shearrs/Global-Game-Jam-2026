using Shears;
using UnityEngine;
using UnityEngine.AI;

namespace CultMask.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public partial class EnemyController : MonoBehaviour
    {
        [Auto]
        private NavMeshAgent agent;

        public void SetDestination(Vector3 destination)
        {
            agent.SetDestination(destination);
        }

        public void ToggleRotation(bool toggle)
        {
            agent.updateRotation = toggle;
        }
    }
}
