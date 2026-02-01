using Shears;
using Shears.StateMachineGraphs;
using System;
using UnityEngine;

namespace CultMask.Enemies
{
    [RequireComponent(typeof(StateMachine), typeof(Enemy))]
    [DefaultExecutionOrder(-200)]
    [CustomWrapper(DisplayFields = new string[] { "stateTree" })]
    public class EnemyStateMachine : ManagedWrapper<StateMachine>
    {
        private Enemy enemy;

        private StateMachine StateMachine => TypedWrappedValue;
        private EnemyData Data => enemy.Data;
        private EnemyStateFlags Flags => enemy.StateFlags;

        public event Action<State> EnteredState { add => StateMachine.EnteredState += value; remove => StateMachine.EnteredState -= value; }
        public event Action<State> ExitedState { add => StateMachine.ExitedState += value; remove => StateMachine.ExitedState -= value; }

        private void Awake()
        {
            StateMachine.UseGraphData = false;
        }

        public void InitializeStates()
        {
            enemy = GetComponent<Enemy>();

            var patrolState = new EnemyPatrolState();
            var chaseState = new EnemyChaseState();
            var attackState = new EnemyAttackState();

            var states = new EnemyState[]
            {
                patrolState,
                chaseState,
                attackState,
            };

            patrolState.AddTransition(() => Flags.Target != null, chaseState);
            chaseState.AddTransition(() => Flags.Target == null, patrolState);
            chaseState.AddTransition(() => Flags.DistanceFromTarget <= Data.TargetDistance && Flags.CanAttack, attackState);
            attackState.AddTransition(() => attackState.IsDoneAttacking, chaseState);

            foreach (var state in states)
                state.Initialize(enemy);

            StateMachine.AddStates(states);
            StateMachine.EnterState(patrolState);
        }
    }
}
