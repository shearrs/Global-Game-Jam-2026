using Shears;
using Shears.Input;
using Shears.Tweens;
using Shears.UI;
using System;
using TMPro;
using UnityEngine;

namespace CultMask.UI
{
    public partial class Popup : MonoBehaviour
    {
        [SerializeField]
        private GameObject graphicsContainer;

        [SerializeField]
        private ManagedImage background;

        [SerializeField]
        private TextMeshProUGUI title;

        [SerializeField]
        private TextMeshProUGUI text;

        [SerializeField]
        [AutoEvent(nameof(CanvasButton.Clicked), nameof(Close))]
        private CanvasButton dismissButton;

        private readonly TweenData tweenData = new(0.5f, unscaledTime: true);
        private Tween backgroundTween;
        private Tween titleTween;
        private Tween textTween;
        private bool isOpen = false;

        public string Title { get => title.text; set => title.text = value; }

        public string Text { get => text.text; set => text.text = value; }

        public event Action Closed;

        public void Open()
        {
            dismissButton.Selectable = false;
            background.Modulate = Color.white.With(a: 0.0f);
            title.color = title.color.With(a: 0.0f);
            text.color = text.color.With(a: 0.0f);
            graphicsContainer.SetActive(true);
            dismissButton.Disable();

            backgroundTween.Dispose();
            titleTween.Dispose();
            textTween.Dispose();

            backgroundTween = background.DoModulateTween(Color.white, tweenData);
            titleTween = title.DoColorTween(title.color.With(a: 1.0f), tweenData);
            textTween = text.DoColorTween(text.color.With(a: 1.0f), tweenData);
            textTween.Completed += () =>
            {
                dismissButton.FadeIn(1.0f, unscaledTime: true);
                dismissButton.FadeInCompleted += () =>
                {
                    dismissButton.Selectable = true;
                    dismissButton.Focus();
                };
            };

            isOpen = true;
        }

        public void Close()
        {
            CursorManager.SetCursorVisibility(false);
            CursorManager.SetCursorLockMode(CursorLockMode.Locked);

            dismissButton.Selectable = false;
            backgroundTween.Dispose();
            titleTween.Dispose();
            textTween.Dispose();

            backgroundTween = background.DoModulateTween(Color.white.With(a: 0.0f), tweenData);
            titleTween = title.DoColorTween(title.color.With(a: 0.0f), tweenData);
            textTween = text.DoColorTween(text.color.With(a: 0.0f), tweenData);
            dismissButton.FadeOut(1.0f, true);

            dismissButton.FadeOutCompleted += () =>
            {
                graphicsContainer.SetActive(false);
                Closed?.Invoke();
            };

            isOpen = false;
        }

        private void Update()
        {
            if (!isOpen)
                return;

            if (ManagedPointer.Current.Delta.sqrMagnitude > 0.0f)
            {
                CursorManager.SetCursorVisibility(true);
                CursorManager.SetCursorLockMode(CursorLockMode.None);
            }
        }
    }
}
