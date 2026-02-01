using Shears;
using Shears.Detection;
using UnityEngine;

namespace CultMask.Enemies
{
    public class EnemyStateFlags : MonoBehaviour
    {
        [SerializeField, ReadOnly]
        private Transform target;

        [SerializeField, ReadOnly]
        private float distanceFromTarget;

        private Enemy enemy;

        private AreaDetector3D TargetDetector => enemy.TargetDetector;

        public Transform Target => target;
        public float DistanceFromTarget => distanceFromTarget;

        public void Initialize(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public void UpdateFlags()
        {
            if (TargetDetector.Detect())
            {
                var possibleTarget = TargetDetector.GetDetection(0).transform;

                if (target == null && HasLineOfSight(possibleTarget))
                    target = possibleTarget;
            }
            else
                target = null;

            if (target != null)
                distanceFromTarget = Vector3.Distance(enemy.transform.position, target.position);
        }

        private bool HasLineOfSight(Transform possibleTarget)
        {
            const float VIEW_HEIGHT = 0.5f;

            var detector = enemy.LineOfSightDetector;

            Vector3 heading = (possibleTarget.position + VIEW_HEIGHT * Vector3.up) - detector.transform.position;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance;

            detector.Direction = detector.transform.InverseTransformDirection(direction);
            detector.Distance = distance;

            return !detector.Detect();
        }
    }
}
