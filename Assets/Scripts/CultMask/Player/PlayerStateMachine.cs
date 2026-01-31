using Shears;
using Shears.StateMachineGraphs;
using UnityEngine;

namespace CultMask.Players
{
    [CustomWrapper(ShowAllFields = true)]
    [RequireComponent(typeof(StateMachine), typeof(Player))]
    public class PlayerStateMachine : ManagedWrapper<StateMachine>
    {
        private Player player;

        private StateMachine StateMachine => TypedWrappedValue;
        private PlayerStateFlags Flags => player.StateFlags;

        private void Awake()
        {
            player = GetComponent<Player>();
        }

        public void InitializeStates()
        {
            var groundedState = new PlayerHubState();
            var idleState = new PlayerHubState();
            var walkState = new PlayerWalkState();

            groundedState.AddSubState(idleState);
            groundedState.AddSubState(walkState);
            groundedState.DefaultSubState = idleState;

            var states = new PlayerState[]
            {
                groundedState,
                idleState,
                walkState,
            };

            idleState.AddTransition(() => Flags.MoveInputMagnitude > 0.01f, walkState);
            walkState.AddTransition(() => Flags.MoveInputMagnitude <= 0.01f, idleState);

            StateMachine.AddStates(states);

            foreach (var state in states)
                state.Initialize(player);
        }
    }
}
