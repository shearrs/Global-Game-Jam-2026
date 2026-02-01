using Shears;
using Shears.StateMachineGraphs;
using Shears.Tweens;
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
        private static readonly int ANIM_IS_HANGING = Animator.StringToHash("isHanging");
        private static readonly int ANIM_JUMP = Animator.StringToHash("jump");
        private static readonly int ANIM_DASH = Animator.StringToHash("dash");
        private static readonly int ANIM_HANG = Animator.StringToHash("hang");
        private static readonly int ANIM_LEFT_PUNCH = Animator.StringToHash("leftPunch");
        private static readonly int ANIM_RIGHT_PUNCH = Animator.StringToHash("rightPunch");

        [SerializeField]
        private PlayerCharacter character;

        [SerializeField]
        private PlayerModel model;

        [SerializeField]
        [AutoEvent(nameof(PlayerStateMachine.EnteredState), nameof(OnStateEntered))]
        [AutoEvent(nameof(PlayerStateMachine.ExitedState), nameof(OnStateExited))]
        private PlayerStateMachine stateMachine;

        [Auto]
        private Animator animator;

        private readonly TweenData upperBodyTweenData = new(0.25f);
        private Tween upperBodyTween;
        private bool wasPunching = false;
        private bool wasLeftPunch = false;

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
            animator.SetBool(ANIM_IS_HANGING, Flags.IsHanging);

            if (character.StateFlags.IsPunching || character.StateFlags.IsPunchWindingDown)
            {
                upperBodyTween.Dispose();
                animator.SetLayerWeight(1, 1.0f);
                wasPunching = true;
            }
            else if (wasPunching)
            {
                wasPunching = false;

                upperBodyTween.Dispose();
                upperBodyTween = TweenManager.DoTween(t =>
                {
                    animator.SetLayerWeight(1, 1.0f - t);
                }, upperBodyTweenData).WithLifetime(this);
            }
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
            else if (state is PlayerLedgeHangState)
            {
                animator.SetTrigger(ANIM_HANG);
                SetLedgePosition();
            }
            else if (state is PlayerPunchState)
            {
                if (wasLeftPunch)
                    animator.SetTrigger(ANIM_RIGHT_PUNCH);
                else
                    animator.SetTrigger(ANIM_LEFT_PUNCH);

                wasLeftPunch = !wasLeftPunch;
            }
        }

        private void OnStateExited(State state)
        {
            if (state is PlayerDashState)
                animator.SetBool(ANIM_IS_DASHING, false);
            else if (state is PlayerLedgeHangState)
                model.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        private void SetLedgePosition()
        {
            var position = (0.35f * character.Data.LedgeHangDistance * Vector3.back) + (0.215f * character.Data.CharacterHeight * Vector3.down);
            var rotation = Quaternion.Euler(new(15, 0, 0));

            model.transform.SetLocalPositionAndRotation(position, rotation);
        }
    }
}
