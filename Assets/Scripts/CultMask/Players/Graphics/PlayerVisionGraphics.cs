using Shears;
using Shears.Tweens;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace CultMask.Players.Graphics
{
    [RequireComponent(typeof(PlayerVisionManager))]
    public partial class PlayerVisionGraphics : MonoBehaviour
    {
        [SerializeField]
        private Volume volume;

        [SerializeField]
        private Color visionVignetteColor = Color.white;

        [SerializeField, Range(0, 1)]
        private float visionVignetteIntensity = 0.5f;

        [SerializeField, Range(0, 1)]
        private float visionAberration = 0.25f;

        [Auto]
        [AutoEvent(nameof(PlayerVisionManager.VisionActivated), nameof(OnVisionActivated))]
        [AutoEvent(nameof(PlayerVisionManager.VisionDeactivated), nameof(OnVisionDeactivated))]
        private PlayerVisionManager visionManager;

        private readonly TweenData tweenData = new(1.0f);
        private Vignette vignette;
        private ChromaticAberration aberration;
        private Color originalVignetteColor;
        private float originalVignetteIntensity;
        private float originalAbberation;
        private Tween tween;

        private void Start()
        {
            if (volume.profile.TryGet(out vignette))
            {
                originalVignetteColor = vignette.color.value;
                originalVignetteIntensity = vignette.intensity.value;
            }

            if (volume.profile.TryGet(out aberration))
                originalAbberation = aberration.intensity.value;
        }

        private void OnVisionActivated()
        {
            tween.Dispose();
            tween = TweenManager.DoTween(t => UpdateVignette(
                t, 
                vignette.color.value, visionVignetteColor, 
                vignette.intensity.value, visionVignetteIntensity,
                aberration.intensity.value, visionAberration
                ), tweenData).WithLifetime(this);
        }

        private void OnVisionDeactivated()
        {
            tween.Dispose();
            tween = TweenManager.DoTween(t => UpdateVignette(
                t, 
                vignette.color.value, originalVignetteColor, 
                vignette.intensity.value, originalVignetteIntensity,
                aberration.intensity.value, originalAbberation
                ), tweenData).WithLifetime(this);
        }

        private void UpdateVignette(
            float t, 
            Color startColor, Color endColor, 
            float startIntensity, float endIntensity,
            float startAberration, float endAberration)
        {
            vignette.color.value = Color.LerpUnclamped(startColor, endColor, t);
            vignette.intensity.value = Mathf.LerpUnclamped(startIntensity, endIntensity, t);
            aberration.intensity.value = Mathf.LerpUnclamped(startAberration, endAberration, t);
        }
    }
}
