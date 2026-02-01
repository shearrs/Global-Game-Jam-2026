using Shears;
using Shears.StateMachineGraphs;
using UnityEngine;

namespace CultMask.Players.Graphics
{
    public partial class PlayerDashGraphics : MonoBehaviour
    {
        [SerializeField]
        [AutoEvent(nameof(PlayerStateMachine.EnteredState), nameof(OnEnteredState))]
        [AutoEvent(nameof(PlayerStateMachine.ExitedState), nameof(OnExitedState))]
        private PlayerStateMachine stateMachine;

        [SerializeField]
        private ParticleSystem dashParticles;

        [SerializeField]
        private TrailRenderer trail;

        private void OnEnteredState(State state)
        {
            if (state is PlayerDashState)
            {
                dashParticles.Play();
                trail.emitting = true;
            }
        }

        private void OnExitedState(State state)
        {
            if (state is PlayerDashState)
                trail.emitting = false;
        }
    }
}
