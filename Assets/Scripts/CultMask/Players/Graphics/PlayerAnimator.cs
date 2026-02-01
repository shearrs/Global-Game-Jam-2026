using Shears;
using Shears.StateMachineGraphs;
using UnityEngine;

namespace CultMask.Players.Graphics
{
    [RequireComponent(typeof(Animator))]
    public partial class PlayerAnimator : MonoBehaviour
    {
        private static readonly int ANIM_MOVE_AMOUNT = Animator.StringToHash("moveAmount");
        private static readonly int ANIM_IS_GROUNDED = Animator.StringToHash("isGrounded");
        private static readonly int ANIM_IS_JUMPING = Animator.StringToHash("isJumping");
        private static readonly int ANIM_IS_DASHING = Animator.StringToHash("isDashing");
        private static readonly int ANIM_JUMP = Animator.StringToHash("jump");
        private static readonly int ANIM_DASH = Animator.StringToHash("dash");

        [SerializeField]
        private PlayerCharacter character;

        [SerializeField]
        [AutoEvent(nameof(PlayerStateMachine.EnteredState), nameof(OnStateEntered))]
        [AutoEvent(nameof(PlayerStateMachine.ExitedState), nameof(OnStateExited))]
        private PlayerStateMachine stateMachine;

        [Auto]
        private Animator animator;

        private PlayerStateFlags Flags => character.StateFlags;

        private void Update()
        {
            UpdateFlags();
        }

        private void UpdateFlags()
        {
            var horizontalVelocity = character.Controller.Velocity.With(y: 0.0f);

            animator.SetFloat(ANIM_MOVE_AMOUNT, horizontalVelocity.magnitude / character.Data.MaxWalkSpeed);
            animator.SetBool(ANIM_IS_GROUNDED, Flags.IsGrounded);
            animator.SetBool(ANIM_IS_JUMPING, Flags.IsJumping);
        }

        private void OnStateEntered(State state)
        {
            if (state is PlayerJumpState || state is PlayerDoubleJumpState)
                animator.SetTrigger(ANIM_JUMP);
            else if (state is PlayerDashState)
            {
                animator.SetBool(ANIM_IS_DASHING, true);
                animator.SetTrigger(ANIM_DASH);
            }
        }

        private void OnStateExited(State state)
        {
            if (state is PlayerDashState)
                animator.SetBool(ANIM_IS_DASHING, false);
        }
    }
}
