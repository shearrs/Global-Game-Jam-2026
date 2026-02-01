using Shears;
using Shears.HitDetection;
using UnityEngine;

namespace CultMask.Players.Graphics
{
    [RequireComponent(typeof(PlayerHealthManager))]
    public partial class PlayerHealthGraphics : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField]
        private Transform healthContainer;

        [SerializeField]
        private GameObject[] healthSegments;

        [Header("Particles")]
        [SerializeField]
        private ParticleSystem damageParticles;

        [Auto]
        [AutoEvent(nameof(PlayerHealthManager.HealthChanged), nameof(OnHealthChanged))]
        [AutoEvent(nameof(PlayerHealthManager.DamageReceived), nameof(OnDamageReceived))]
        private PlayerHealthManager healthManager;

        private void OnHealthChanged(int health)
        {
            foreach (var segment in healthSegments)
                segment.SetActive(false);

            for (int i = 0; i < health; i++)
                healthSegments[i].SetActive(true);
        }

        private void OnDamageReceived(HitData3D _)
        {
            damageParticles.Play();
        }
    }
}
