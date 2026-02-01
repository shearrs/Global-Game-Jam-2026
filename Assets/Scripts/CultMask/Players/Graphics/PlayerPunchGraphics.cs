using Shears;
using Shears.HitDetection;
using UnityEngine;

namespace CultMask.Players.Graphics
{
    [RequireComponent(typeof(PlayerPunchManager))]
    public partial class PlayerPunchGraphics : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem punchParticles;

        [SerializeField]
        private ParticleSystem impactParticles;

        [Auto]
        [AutoEvent(nameof(PlayerPunchManager.Punched), nameof(OnPunched))]
        [AutoEvent(nameof(PlayerPunchManager.HitDelivered), nameof(OnHitDelivered))]
        private PlayerPunchManager punchManager;

        private void OnPunched()
        {
            punchParticles.Play();
        }

        private void OnHitDelivered(HitData3D data)
        {
            impactParticles.transform.position = data.Result.Point;
            impactParticles.Play();
        }
    }
}
