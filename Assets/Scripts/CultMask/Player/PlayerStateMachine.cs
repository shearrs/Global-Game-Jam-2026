using Shears;
using Shears.StateMachineGraphs;
using System;
using UnityEngine;

namespace CultMask.Players
{
    [RequireComponent(typeof(StateMachine), typeof(PlayerCharacter))]
    [DefaultExecutionOrder(-200)]
    [CustomWrapper(DisplayFields = new string[] { "stateTree" })]
    public class PlayerStateMachine : ManagedWrapper<StateMachine>
    {
        private Player player;

        private PlayerCharacter Character => player.Character;
        private PlayerInput Input => player.Input;
        private StateMachine StateMachine => TypedWrappedValue;
        private PlayerStateFlags Flags => Character.StateFlags;
        private PlayerController Controller => Character.Controller;

        public event Action<State> EnteredState { add => StateMachine.EnteredState += value; remove => StateMachine.EnteredState -= value; }

        private void Awake()
        {
            StateMachine.UseGraphData = false;
        }

        public void InitializeStates()
        {
            player = GetComponent<PlayerCharacter>().Player;

            var groundedState = new PlayerHubState("Grounded");
            var idleState = new PlayerHubState("Idle", true);
            var walkState = new PlayerWalkState();
            var jumpState = new PlayerJumpState();

            var aerialState = new PlayerHubState("Aerial");
            var fallState = new PlayerFallState();

            groundedState.AddSubStates(idleState, walkState, jumpState);
            groundedState.DefaultSubState = idleState;

            aerialState.AddSubState(fallState);
            aerialState.DefaultSubState = fallState;

            var states = new PlayerState[]
            {
                groundedState,
                idleState,
                walkState,
                jumpState,
                aerialState,
                fallState,
            };

            idleState.AddTransition(() => Flags.MoveInputMagnitude > 0.01f, walkState);
            walkState.AddTransition(() => Flags.MoveInputMagnitude <= 0.01f, idleState);
            groundedState.AddTransition(() => Flags.IsGrounded && Input.JumpInput.WasPressedThisFrame(), jumpState);
            jumpState.AddTransition(() => Controller.Velocity.y <= 0, fallState);

            aerialState.AddTransition(() => Flags.IsGrounded, groundedState);

            StateMachine.AddStates(states);

            foreach (var state in states)
                state.Initialize(player);

            StateMachine.EnterState(groundedState);
        }
    }
}
