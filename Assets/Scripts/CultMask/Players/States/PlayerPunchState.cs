using Shears;
using UnityEngine;

namespace CultMask.Players
{
    [System.Serializable]
    public class PlayerPunchState : PlayerState
    {
        public PlayerPunchState()
        {
            Name = "Punch";
        }

        protected override void OnEnter()
        {
            Character.PunchManager.Punch();

            var rotationDirection = GetInputDirection();
            if (rotationDirection == Vector3.zero)
                rotationDirection = Character.transform.forward;

            Character.transform.rotation = Quaternion.LookRotation(rotationDirection, Vector3.up);
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
            AdaptiveApplyGravity();
            AdaptiveUpdateMovement(rotationSpeed: 0.0f);
        }
    }
}
