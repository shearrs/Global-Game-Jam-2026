using Shears;
using Shears.HitDetection;
using Shears.Tweens;
using UnityEngine;

namespace CultMask.Levels
{
    public class DestructibleElement : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private HurtBody3D hurtBody;

        [SerializeField]
        private ParticleSystem destroyParticles;

        [Header("Settings")]
        [SerializeField, Min(1)]
        private int health = 1;

        [SerializeField, Min(0)]
        private float shakeStrength = .15f;

        [SerializeField, Min(0.001f)]
        private float shakeDelay = 0.05f;

        [SerializeField]
        private TweenData shakeTweenData = new(0.2f);

        private Tween shakeTween;

        private void Awake()
        {
            hurtBody.HitReceived += OnHitReceived;
        }

        private void OnHitReceived(HitData3D data)
        {
            health -= 1;

            if (health <= 0)
            {
                destroyParticles.transform.SetParent(null);
                destroyParticles.Play();
                CoroutineUtil.DoAfter(() => Destroy(destroyParticles), destroyParticles.main.duration, destroyParticles);

                Destroy(gameObject);
            }
            else
            {
                shakeTween.Dispose();
                shakeTween = transform.DoShakeTween(shakeStrength, shakeDelay, shakeTweenData);
                destroyParticles.Play();
            }
        }
    }
}
