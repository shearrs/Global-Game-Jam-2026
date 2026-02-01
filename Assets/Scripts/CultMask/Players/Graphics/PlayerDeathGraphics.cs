using Shears;
using Shears.Tweens;
using Shears.UI;
using UnityEngine;

namespace CultMask.Players.Graphics
{
    public partial class PlayerDeathGraphics : MonoBehaviour
    {
        private const float FADE_DELAY = 1.0f;

        [SerializeField]
        [AutoEvent(nameof(Player.CharacterSpawned), nameof(OnCharacterSpawned))]
        private Player player;

        [SerializeField]
        private ManagedImage fadeImage;

        [SerializeField]
        private ParticleSystem deathParticles;

        private readonly TweenData fadeOutTween = new(2.0f, unscaledTime: true);
        private readonly TweenData fadeInTween = new(3.0f, unscaledTime: true);
        private Tween tween;
        private PlayerCharacter character;

        private void OnCharacterSpawned(PlayerCharacter character)
        {
            tween.Dispose();
            fadeImage.gameObject.SetActive(true);
            fadeImage.Modulate = Color.white;
            tween = fadeImage.DoModulateTween(Color.white.With(a: 0.0f), fadeInTween);
            tween.Completed += () => fadeImage.gameObject.SetActive(false);

            this.character = character;
            character.Died += OnCharacterDied;
        }

        private void OnCharacterDied()
        {
            tween.Dispose();
            fadeImage.gameObject.SetActive(true);
            tween = fadeImage.GetModulateTween(Color.white, fadeOutTween);
            tween.PlayAfter(FADE_DELAY);

            deathParticles.transform.position = character.transform.position + Vector3.up;
            deathParticles.Play();
        }
    }
}
