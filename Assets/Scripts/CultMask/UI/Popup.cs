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
        private Tween buttonTween;

        public string Title { get => title.text; set => title.text = value; }

        public string Text { get => text.text; set => text.text = value; }

        public event Action Closed;

        public void Open()
        {
            CursorManager.SetCursorVisibility(true);
            CursorManager.SetCursorLockMode(CursorLockMode.None);

            dismissButton.Selectable = false;
            dismissButton.Image.BaseColor = dismissButton.Image.BaseColor.With(a: 0.0f);
            background.Modulate = Color.white.With(a: 0.0f);
            title.color = title.color.With(a: 0.0f);
            text.color = text.color.With(a: 0.0f);
            graphicsContainer.SetActive(true);

            backgroundTween.Dispose();
            titleTween.Dispose();
            textTween.Dispose();

            backgroundTween = background.DoModulateTween(Color.white, tweenData);
            titleTween = title.DoColorTween(title.color.With(a: 1.0f), tweenData);
            textTween = text.DoColorTween(text.color.With(a: 1.0f), tweenData);
            textTween.Completed += () =>
            {
                buttonTween = dismissButton.Image.DoColorTween(dismissButton.Image.BaseColor.With(a: 1.0f), tweenData);
                buttonTween.Completed += () => dismissButton.Selectable = true;
            };
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
            buttonTween = dismissButton.Image.DoColorTween(dismissButton.Image.BaseColor.With(a: 0.0f), tweenData);

            buttonTween.Completed += () =>
            {
                graphicsContainer.SetActive(false);
                Closed?.Invoke();
            };
        }
    }
}
