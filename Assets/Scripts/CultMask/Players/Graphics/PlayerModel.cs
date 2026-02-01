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

        [SerializeField]
        private Transform leftHand;

        [SerializeField]
        private Transform rightHand;

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

        private void Awake()
        {
            originalLeftHandPosition = leftHand.localPosition;
            originalRightHandPosition = rightHand.localPosition;
        }

        private void Update()
        {
            AdjustBodyHeight();

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
            if (state is PlayerLedgeHangState)
                AnimateLedgeArms();
            else if (state is PlayerJumpState || state is PlayerDoubleJumpState)
                DoJumpStretch();
        }

        private void OnStateExited(State state)
        {
            if (state is PlayerLedgeHangState)
            {
                leftHand.transform.localPosition = originalLeftHandPosition;
                rightHand.transform.localPosition = originalRightHandPosition;
            }
        }

        private void AnimateLedgeArms()
        {
            var data = character.Data;

            leftHand.transform.localPosition = originalLeftHandPosition + new Vector3(0.25f, 0.6f * data.CharacterHeight, data.LedgeHangDistance);
            rightHand.transform.localPosition = originalRightHandPosition + new Vector3(-0.25f, 0.6f * data.CharacterHeight, data.LedgeHangDistance);
        }

        private void DoJumpStretch()
        {
            const float STRETCH_AMOUNT = 1.15f;

            bodyTween.Dispose();
            bodyTween = body.DoScaleLocalTween(new(0.85f, STRETCH_AMOUNT, 0.85f), jumpStretchData);
            bodyTween.Completed += () => bodyTween = body.DoScaleLocalTween(Vector3.one, returnData);
        }

        private void DoFallSquash()
        {
            const float SQUASH_AMOUNT = 0.85f;

            bodyTween.Dispose();
            bodyTween = body.DoScaleLocalTween(new(1.15f, SQUASH_AMOUNT, 1.15f), fallSquashData);
            bodyTween.Completed += () => bodyTween = body.DoScaleLocalTween(Vector3.one, returnData);
        }

        private void AdjustBodyHeight()
        {
            //if (bodyTween.IsPlaying)
            //    body.transform.localPosition = body.transform.localPosition.With(y: body.transform.localScale.y);
            //else
            //    body.transform.localPosition = Vector3.zero;
        }
    }
}
