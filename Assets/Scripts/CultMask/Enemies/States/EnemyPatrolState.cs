using Shears;
using UnityEngine;

namespace CultMask.Enemies
{
    [System.Serializable]
    public class EnemyPatrolState : EnemyState
    {
        private static readonly Range<float> PATROL_DELAY = new(2.0f, 4.0f);
        private static readonly Range<float> PATROL_DISTANCE = new(2f, 5.0f);

        private readonly Timer patrolTimer = new();

        public EnemyPatrolState()
        {
            Name = "Patrol";
        }

        protected override void OnEnter()
        {
            UpdatePatrol();

            patrolTimer.Completed += UpdatePatrol;
        }

        protected override void OnExit()
        {
            patrolTimer.Completed -= UpdatePatrol;
        }

        protected override void OnUpdate()
        {
        }

        private void UpdatePatrol()
        {
            float targetDistance = PATROL_DISTANCE.Random();
            Vector3 targetDirection = Random.insideUnitCircle.normalized;
            targetDirection.z = targetDirection.y;
            targetDirection.y = 0;

            Vector3 targetOffset = targetDistance * targetDirection;

            Controller.SetDestination(Enemy.transform.position + targetOffset);

            patrolTimer.Start(PATROL_DELAY.Random());
        }
    }
}
