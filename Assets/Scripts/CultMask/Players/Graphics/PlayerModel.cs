using Shears;
using Shears.StateMachineGraphs;
using Shears.Tweens;
using UnityEngine;

namespace CultMask.Players.Graphics
{
    public partial class PlayerModel : MonoBehaviour
    {
        #region Variables
        private const float SQUASH_VELOCITY_THRESHOLD = -1.0f;

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

        private Tween bodyTween;
        private Tween leftHandTween;
        private Tween rightHandTween;
        #endregion

        private void Awake()
        {
            originalLeftHandPosition = leftHand.localPosition;
            originalRightHandPosition = rightHand.localPosition;
        }

        private void Update()
        {
            AdjustBodyHeight();                
        }

        private void OnStateEntered(State state)
        {
            if (state is PlayerLedgeHangState)
                AnimateLedgeArms();
            else if (state is PlayerJumpState)
                DoJumpStretch();
        }

        private void OnStateExited(State state)
        {
            if (state is PlayerLedgeHangState)
            {
                leftHand.transform.localPosition = originalLeftHandPosition;
                rightHand.transform.localPosition = originalRightHandPosition;
            }
            else if (state is PlayerFallState)
            {
                if (character.Controller.Velocity.y < SQUASH_VELOCITY_THRESHOLD)
                    DoFallSquash();
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
            if (bodyTween.IsPlaying)
                body.transform.localPosition = body.transform.localPosition.With(y: body.transform.localScale.y);
            else
                body.transform.localPosition = new(0.0f, 1.0f, 0.0f);
        }
    }
}
