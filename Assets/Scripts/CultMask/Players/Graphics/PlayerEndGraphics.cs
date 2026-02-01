using Shears;
using Shears.Detection;
using Shears.Input;
using Shears.Tweens;
using Shears.UI;
using TMPro;
using UnityEngine;

namespace CultMask.Players.Graphics
{
    public class PlayerEndGraphics : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private Player player;

        [SerializeField]
        private AreaDetector3D endDetector;

        [Header("UI")]
        [SerializeField]
        private GameObject graphicsContainer;

        [SerializeField]
        private ManagedImage fadeIn;

        [SerializeField]
        private TextMeshProUGUI endText;

        [SerializeField]
        private CanvasButton exitButton;

        private bool isEnding;

        private void Update()
        {
            if (isEnding)
            {
                if (ManagedPointer.Current.Delta.sqrMagnitude > 0.0f)
                {
                    CursorManager.SetCursorVisibility(true);
                    CursorManager.SetCursorLockMode(CursorLockMode.None);
                }

                return;
            }

            if (endDetector.Detect())
                StartEndSequence();
        }

        private void StartEndSequence()
        {
            isEnding = true;
            graphicsContainer.SetActive(true);

            player.IsEnding = true;
            player.Input.Disable();

            fadeIn.Modulate = Color.white.With(a: 0.0f);
            endText.color = endText.color.With(a: 0.0f);
            exitButton.Disable();

            var fadeTween = fadeIn.DoModulateTween(Color.white);
            fadeTween.Completed += () => endText.DoColorTween(endText.color.With(a: 1.0f));
            fadeTween.Completed += () => exitButton.FadeIn(1.0f);
            fadeTween.Completed += exitButton.Focus;
        }
    }
}
