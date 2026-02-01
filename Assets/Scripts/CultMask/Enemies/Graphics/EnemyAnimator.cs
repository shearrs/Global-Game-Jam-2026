using Shears;
using Shears.StateMachineGraphs;
using UnityEngine;

namespace CultMask.Enemies.Graphics
{
    [RequireComponent(typeof(Animator))]
    public partial class EnemyAnimator : MonoBehaviour
    {
        private static readonly int ANIM_MOVE_AMOUNT = Animator.StringToHash("moveAmount");
        private static readonly int ANIM_ATTACK = Animator.StringToHash("attack");

        [SerializeField]
        private Enemy enemy;

        [SerializeField]
        [AutoEvent(nameof(EnemyStateMachine.EnteredState), nameof(OnStateEntered))]
        [AutoEvent(nameof(EnemyStateMachine.ExitedState), nameof(OnStateExited))]
        private EnemyStateMachine stateMachine;

        [Auto]
        private Animator animator;

        private void Update()
        {
            UpdateFlags();
        }

        private void UpdateFlags()
        {
            animator.SetFloat(ANIM_MOVE_AMOUNT, enemy.Controller.Velocity.magnitude / enemy.Controller.MoveSpeed);
        }

        private void OnStateEntered(State state)
        {
            if (state is EnemyAttackState)
                animator.SetTrigger(ANIM_ATTACK);
        }

        private void OnStateExited(State state)
        {

        }
    }
}
