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

            Character.transform.rotation = Quaternion.LookRotation(Camera.transform.forward.With(y: 0.0f), Vector3.up);
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
            AdaptiveApplyGravity();
            AdaptiveUpdateMovement(rotationSpeed: 0.0f, maxSpeed: 0.75f * Data.MaxWalkSpeed);
        }
    }
}
