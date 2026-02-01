using Shears;
using Shears.HitDetection;
using UnityEngine;

namespace CultMask.Players.Graphics
{
    [RequireComponent(typeof(PlayerHealthManager))]
    public partial class PlayerHealthGraphics : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem damageParticles;

        [Auto]
        [AutoEvent(nameof(PlayerHealthManager.DamageReceived), nameof(OnDamageReceived))]
        private PlayerHealthManager healthManager;

        private void OnDamageReceived(HitData3D _)
        {
            damageParticles.Play();
        }
    }
}
