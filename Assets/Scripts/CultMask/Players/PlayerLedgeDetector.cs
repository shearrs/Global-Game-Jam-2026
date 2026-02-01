using Shears;
using Shears.Detection;
using UnityEngine;

namespace CultMask.Players
{
    public class PlayerLedgeDetector : MonoBehaviour
    {
        [SerializeField]
        private RayDetector3D ledgeDetector;

        [SerializeField]
        private SphereDetector occlusionDetector;

        [SerializeField]
        private RayDetector3D wallDetector;

        private bool isLedgeDetected;
        private float ledgeHeight;
        private Vector3 wallPoint;
        private Vector3 wallNormal;

        public bool IsLedgeDetected => isLedgeDetected;
        public float LedgeHeight => ledgeHeight;
        public Vector3 WallPoint => wallPoint;
        public Vector3 WallNormal => wallNormal;

        private void Update()
        {
            UpdateLedgeDetection();
        }

        private void UpdateLedgeDetection()
        {
            isLedgeDetected = false;

            if (!ledgeDetector.Detect())
                return;

            if (occlusionDetector.Detect())
                return;

            if (!wallDetector.Detect())
                return;

            var ledgeHit = ledgeDetector.GetHit(0);
            var wallHit = wallDetector.GetHit(0);

            ledgeHeight = ledgeHit.point.y;
            wallPoint = wallHit.point;
            wallNormal = wallHit.normal.normalized;
            isLedgeDetected = true;
        }
    }
}
