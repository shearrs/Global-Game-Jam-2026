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
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
            AdaptiveApplyGravity();
            AdaptiveUpdateMovement();
        }
    }
}
