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

            var locomotionState = new PlayerHubState("Locomotion");

            var groundedState = new PlayerGroundedState();
            var idleState = new PlayerHubState("Idle", true);
            var walkState = new PlayerWalkState();
            var jumpState = new PlayerJumpState();

            var aerialState = new PlayerHubState("Aerial");
            var fallState = new PlayerFallState();
            var doubleJumpState = new PlayerDoubleJumpState();

            var controlledState = new PlayerHubState("Controlled");
            var ledgeHangState = new PlayerLedgeHangState();
            var dashState = new PlayerDashState();

            groundedState.AddSubStates(idleState, walkState, jumpState);
            groundedState.DefaultSubState = idleState;

            aerialState.AddSubStates(fallState, doubleJumpState);
            aerialState.DefaultSubState = fallState;

            controlledState.AddSubStates(ledgeHangState, dashState);
            controlledState.DefaultSubState = ledgeHangState;

            locomotionState.AddSubStates(groundedState, aerialState, controlledState);
            locomotionState.DefaultSubState = groundedState;

            var states = new PlayerState[]
            {
                locomotionState,
                groundedState,
                idleState,
                walkState,
                jumpState,
                aerialState,
                fallState,
                doubleJumpState,
                controlledState,
                ledgeHangState,
                dashState
            };

            idleState.AddTransition(() => Flags.MoveInputMagnitude > 0.01f, walkState);
            walkState.AddTransition(() => Flags.MoveInputMagnitude <= 0.01f, idleState);
            groundedState.AddTransition(() => !Flags.IsJumping && Flags.IsJumpBuffered, jumpState);
            groundedState.AddTransition(() => !Flags.IsGrounded && !Flags.IsJumping, aerialState);
            groundedState.AddTransition(() => Flags.CanDash && Flags.IsDashBuffered, dashState);
            jumpState.AddTransition(() => Controller.Velocity.y <= 0, fallState);
            jumpState.AddTransition(() => Flags.CanDoubleJump && Flags.IsJumpBuffered, doubleJumpState);

            aerialState.AddTransition(() => Flags.IsGrounded, groundedState);
            fallState.AddTransition(() => Flags.IsDetectingLedge, ledgeHangState);
            fallState.AddTransition(() => Flags.CanDash && Flags.IsDashBuffered, dashState);
            fallState.AddTransition(() => Flags.CanDoubleJump && Flags.IsJumpBuffered, doubleJumpState);
            doubleJumpState.AddTransition(() => Controller.Velocity.y <= 0, fallState);

            ledgeHangState.AddTransition(() => Flags.IsJumpBuffered, jumpState);
            ledgeHangState.AddTransition(() => Input.DropFromLedgeInput.WasPressedThisFrame(), fallState);

            dashState.AddTransition(() => Flags.CanStopDashing && Controller.Velocity.y <= 0, fallState);
            dashState.AddTransition(() => Flags.IsDetectingLedge, ledgeHangState);
            dashState.AddTransition(() => !Flags.IsGrounded && Flags.CanDoubleJump && Flags.IsJumpBuffered, doubleJumpState);

            StateMachine.AddStates(states);

            foreach (var state in states)
                state.Initialize(player);

            StateMachine.EnterState(groundedState);
        }

        public bool IsInStateOfType<T>() where T : State => StateMachine.IsInStateOfType<T>();
    }
}
