using UnityEngine;

namespace CultMask.Players
{
    [System.Serializable]
    public class PlayerHubState : PlayerState
    {
        private readonly bool updateMovement;

        public PlayerHubState(string name, bool updateMovement = false)
        {
            Name = name;
            this.updateMovement = updateMovement;
        }

        protected override void OnEnter()
        {
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
            if (updateMovement)
                StandardUpdateMovement();
        }
    }
}
