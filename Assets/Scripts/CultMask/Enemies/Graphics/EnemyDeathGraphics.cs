using Shears;
using UnityEngine;

namespace CultMask.Enemies.Graphics
{
    public partial class EnemyDeathGraphics : MonoBehaviour
    {
        [SerializeField]
        [AutoEvent(nameof(Enemy.Died), nameof(OnEnemyDied))]
        private Enemy enemy;

        [SerializeField]
        private ParticleSystem deathParticles;

        private void OnEnemyDied()
        {
            deathParticles.transform.SetParent(null);
            deathParticles.Play();

            CoroutineUtil.DoAfter(() => Destroy(deathParticles.gameObject), deathParticles.main.duration, deathParticles);
        }
    }
}
