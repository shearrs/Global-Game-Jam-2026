using Shears;
using Shears.StateMachineGraphs;
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
        private EnemyStateFlags Flags => enemy.StateFlags;

        private void Awake()
        {
            StateMachine.UseGraphData = false;
        }

        public void InitializeStates()
        {
            enemy = GetComponent<Enemy>();

            var patrolState = new EnemyPatrolState();
            var chaseState = new EnemyChaseState();

            var states = new EnemyState[]
            {
                patrolState,
                chaseState,
            };

            patrolState.AddTransition(() => Flags.Target != null, chaseState);
            chaseState.AddTransition(() => Flags.Target == null, patrolState);

            foreach (var state in states)
                state.Initialize(enemy);

            StateMachine.AddStates(states);
            StateMachine.EnterState(patrolState);
        }
    }
}
