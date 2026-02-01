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

        [Auto]
        [AutoEvent(nameof(PlayerPunchManager.HitDelivered), nameof(OnHitDelivered))]
        private PlayerPunchManager punchManager;

        private void OnHitDelivered(HitData3D data)
        {
            punchParticles.transform.position = data.Result.Point;
            punchParticles.Play();
        }
    }
}
