using UnityEngine;

namespace CultMask.Players
{
    [System.Serializable]
    public class PlayerFallState : PlayerState
    {
        public PlayerFallState()
        {
            Name = "Fall";
        }

        protected override void OnEnter()
        {
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
            Controller.AddVelocity(Data.Gravity * Time.deltaTime * Vector3.up);

            if (Flags.HasDashed)
                StandardUpdateMovement(moveAcceleration: Data.DashControlAcceleration, moveDeceleration: Data.DashControlDeceleration, maxSpeed: Data.DashMaxSpeed);
            else
                StandardUpdateMovement();
        }
    }
}
