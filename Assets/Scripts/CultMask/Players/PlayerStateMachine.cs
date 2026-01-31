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
        public event Action<State> ExitedState { add => StateMachine.ExitedState += value; remove => StateMachine.ExitedState -= value; }

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

            var controlledState = new PlayerHubState("Controlled");
            var ledgeHangState = new PlayerLedgeHangState();

            groundedState.AddSubStates(idleState, walkState, jumpState);
            groundedState.DefaultSubState = idleState;

            aerialState.AddSubState(fallState);
            aerialState.DefaultSubState = fallState;

            controlledState.AddSubState(ledgeHangState);
            controlledState.DefaultSubState = ledgeHangState;

            var states = new PlayerState[]
            {
                groundedState,
                idleState,
                walkState,
                jumpState,
                aerialState,
                fallState,
                controlledState,
                ledgeHangState,
            };

            idleState.AddTransition(() => Flags.MoveInputMagnitude > 0.01f, walkState);
            walkState.AddTransition(() => Flags.MoveInputMagnitude <= 0.01f, idleState);
            groundedState.AddTransition(() => Flags.IsGrounded && Input.JumpInput.WasPressedThisFrame(), jumpState);
            groundedState.AddTransition(() => !Flags.IsGrounded && !Flags.IsJumping, aerialState);
            jumpState.AddTransition(() => Controller.Velocity.y <= 0, fallState);

            aerialState.AddTransition(() => Flags.IsGrounded, groundedState);
            fallState.AddTransition(() => Flags.IsDetectingLedge, ledgeHangState);

            ledgeHangState.AddTransition(() => Input.JumpInput.WasPressedThisFrame(), jumpState);
            ledgeHangState.AddTransition(() => Input.DropFromLedgeInput.WasPressedThisFrame(), fallState);

            StateMachine.AddStates(states);

            foreach (var state in states)
                state.Initialize(player);

            StateMachine.EnterState(groundedState);
        }
    }
}
