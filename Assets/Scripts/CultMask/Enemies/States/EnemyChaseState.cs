using Shears;
using UnityEngine;

namespace CultMask.Enemies
{
    [System.Serializable]
    public class EnemyChaseState : EnemyState
    {
        private const float ROTATION_SPEED = 360.0f;

        public EnemyChaseState()
        {
            Name = "Chase";
        }

        protected override void OnEnter()
        {
            Controller.ToggleRotation(false);
        }

        protected override void OnExit()
        {
            Controller.ToggleRotation(true);
        }

        protected override void OnUpdate()
        {
            if (Flags.Target == null)
                return;

            Vector3 offsetDirection = (Enemy.transform.position - Flags.Target.position).With(y: 0.0f).normalized;
            Vector3 targetPosition = Flags.Target.position + (0.5f * Data.TargetDistance * offsetDirection);
            Quaternion targetRotation = Quaternion.LookRotation(-offsetDirection, Vector3.up);

            Controller.SetDestination(targetPosition);
            Enemy.transform.rotation = Quaternion.RotateTowards(Enemy.transform.rotation, targetRotation, Time.deltaTime * ROTATION_SPEED);
        }
    }
}
