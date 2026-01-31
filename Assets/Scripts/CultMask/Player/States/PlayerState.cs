using Shears.StateMachineGraphs;
using UnityEngine;

namespace CultMask.Players
{
    public abstract class PlayerState : State
    {
        protected Player Player { get; private set; }
        protected PlayerData Data => Player.Data;
        protected PlayerInput Input => Player.Input;
        protected PlayerCamera Camera => Player.Camera;
        protected PlayerController Controller => Player.Controller;

        public void Initialize(Player player)
        {
            Player = player;
        }
    }
}
