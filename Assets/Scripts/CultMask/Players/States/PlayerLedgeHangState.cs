using UnityEngine;

namespace CultMask.Players
{
    [System.Serializable]
    public class PlayerLedgeHangState : PlayerState
    {
        private Vector3 wallPoint;
        private Vector3 wallNormal;
        private float ledgeHeight;

        public Vector3 WallPoint => wallPoint;
        public Vector3 WallNormal => wallNormal;
        public float LedgeHeight => ledgeHeight;

        public PlayerLedgeHangState()
        {
            Name = "Ledge Hang";
        }

        protected override void OnEnter()
        {
            var ledgeDetector = Character.LedgeDetector;

            wallPoint = ledgeDetector.WallPoint;
            wallNormal = ledgeDetector.WallNormal;
            ledgeHeight = ledgeDetector.LedgeHeight;

            Vector3 hangPosition = wallPoint + (wallNormal * Character.Data.LedgeHangDistance);
            hangPosition.y = ledgeHeight - Data.CharacterHeight;

            Character.transform.SetPositionAndRotation(hangPosition, Quaternion.LookRotation(-wallNormal, Vector3.up));
            Controller.SetVelocity(Vector3.zero);
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
        }
    }
}
