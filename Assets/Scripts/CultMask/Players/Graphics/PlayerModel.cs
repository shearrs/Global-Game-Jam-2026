using Shears;
using Shears.StateMachineGraphs;
using Shears.Tweens;
using System;
using UnityEngine;

namespace CultMask.Players.Graphics
{
    public partial class PlayerModel : MonoBehaviour
    {
        #region Variables
        private const float SQUASH_VELOCITY_THRESHOLD = -4.0f;

        [Header("Player Components")]
        [SerializeField]
        private PlayerCharacter character;

        [SerializeField]
        [AutoEvent(nameof(PlayerStateMachine.EnteredState), nameof(OnStateEntered))]
        [AutoEvent(nameof(PlayerStateMachine.ExitedState), nameof(OnStateExited))]
        private PlayerStateMachine stateMachine;

        [Header("Graphics Components")]
        [SerializeField]
        private Transform body;

        [Header("Tweens")]
        [SerializeField]
        private TweenData jumpStretchData = new(0.1f, easingFunction: TweenEase.OutBack);

        [SerializeField]
        private TweenData fallSquashData = new(0.1f, easingFunction: TweenEase.OutBack);

        [SerializeField]
        private TweenData returnData = new(0.1f);

        private Vector3 originalLeftHandPosition;
        private Vector3 originalRightHandPosition;
        private bool wasGrounded;
        private float previousNonZeroYVelocity;

        private Tween bodyTween;

        public event Action Landed;
        #endregion

        private void Update()
        {
            if (Mathf.Abs(character.Controller.Velocity.y) > 0.01f)
                previousNonZeroYVelocity = character.Controller.Velocity.y;

            if (character.Controller.IsGrounded && !wasGrounded && previousNonZeroYVelocity < SQUASH_VELOCITY_THRESHOLD)
            {
                Landed?.Invoke();

                DoFallSquash();
                previousNonZeroYVelocity = 0;
            }

            wasGrounded = character.Controller.IsGrounded;
        }

        private void OnStateEntered(State state)
        {
            if (state is PlayerJumpState || state is PlayerDoubleJumpState)
                DoJumpStretch();
        }

        private void OnStateExited(State state)
        {
            // don't need this..
        }

        private void DoJumpStretch()
        {
            const float STRETCH_AMOUNT = 1.25f;
            const float INVERSE_STRETCH_AMOUNT = 0.7f;

            bodyTween.Dispose();
            bodyTween = body.DoScaleLocalTween(new(INVERSE_STRETCH_AMOUNT, STRETCH_AMOUNT, INVERSE_STRETCH_AMOUNT), jumpStretchData);
            bodyTween.Completed += () => bodyTween = body.DoScaleLocalTween(Vector3.one, returnData);
        }

        private void DoFallSquash()
        {
            const float SQUASH_AMOUNT = 0.75f;
            const float INVERSE_SQUASH_AMOUNT = 1.25f;

            bodyTween.Dispose();
            bodyTween = body.DoScaleLocalTween(new(INVERSE_SQUASH_AMOUNT, SQUASH_AMOUNT, INVERSE_SQUASH_AMOUNT), fallSquashData);
            bodyTween.Completed += () => bodyTween = body.DoScaleLocalTween(Vector3.one, returnData);
        }
    }
}
