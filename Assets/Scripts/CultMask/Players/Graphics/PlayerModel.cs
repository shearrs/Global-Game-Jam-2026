using Shears;
using Shears.StateMachineGraphs;
using UnityEngine;

namespace CultMask.Players.Graphics
{
    public partial class PlayerModel : MonoBehaviour
    {
        [Header("Player Components")]
        [SerializeField]
        private PlayerCharacter character;

        [SerializeField]
        [AutoEvent(nameof(PlayerStateMachine.EnteredState), nameof(OnStateEntered))]
        [AutoEvent(nameof(PlayerStateMachine.ExitedState), nameof(OnStateExited))]
        private PlayerStateMachine stateMachine;

        [Header("Graphics Components")]
        [SerializeField]
        private Transform leftHand;

        [SerializeField]
        private Transform rightHand;

        private Vector3 originalLeftHandPosition;
        private Vector3 originalRightHandPosition;

        private void Awake()
        {
            originalLeftHandPosition = leftHand.localPosition;
            originalRightHandPosition = rightHand.localPosition;
        }

        private void OnStateEntered(State state)
        {
            if (state is PlayerLedgeHangState)
                AnimateLedgeArms();
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
    }
}
