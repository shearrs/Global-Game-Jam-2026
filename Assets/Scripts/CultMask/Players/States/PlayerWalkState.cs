using Shears;
using UnityEngine;

namespace CultMask.Players
{
    [System.Serializable]
    public class PlayerWalkState : PlayerState
    {
        public PlayerWalkState()
        {
            Name = "Walk";
        }

        protected override void OnEnter()
        {
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
            StandardUpdateMovement();
        }
    }
}
