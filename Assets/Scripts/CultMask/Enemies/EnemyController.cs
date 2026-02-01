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

        private bool updateRotation = true;

        private const float ROTATION_SPEED = 360.0f;

        public Vector3 Velocity => agent.velocity;
        public float MoveSpeed => agent.speed;

        public void SetDestination(Vector3 destination)
        {
            agent.SetDestination(destination);
        }

        public void ToggleRotation(bool toggle)
        {
            updateRotation = toggle;
        }
    }
}
