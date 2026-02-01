using Shears;
using Shears.StateMachineGraphs;
using UnityEngine;

namespace CultMask.Players.Graphics
{
    public partial class PlayerMaskGraphics : MonoBehaviour
    {
        [SerializeField]
        [AutoEvent(nameof(PlayerStateMachine.EnteredState), nameof(OnStateEntered))]
        private PlayerStateMachine stateMachine;

        [SerializeField]
        [AutoEvent(nameof(PlayerVisionManager.VisionActivated), nameof(OnVisionEnabled))]
        private PlayerVisionManager visionManager;

        [SerializeField]
        private GameObject dashMask;

        [SerializeField]
        private GameObject visionMask;

        [SerializeField]
        private GameObject jumpMask;

        private GameObject currentMask;

        private void OnStateEntered(State state)
        {
            if (state is PlayerDashState)
                EnableMask(dashMask);
            else if (state is PlayerDoubleJumpState)
                EnableMask(jumpMask);
        }

        private void OnVisionEnabled()
        {
            EnableMask(visionMask);
        }

        private void EnableMask(GameObject mask)
        {
            if (currentMask != null)
                currentMask.SetActive(false);

            currentMask = mask;

            if (mask != null)
                mask.SetActive(true);
        }
    }
}
