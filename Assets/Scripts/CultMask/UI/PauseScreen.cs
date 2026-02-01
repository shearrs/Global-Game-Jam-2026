using CultMask.Players;
using Shears;
using Shears.Input;
using Shears.UI;
using UnityEngine;

namespace CultMask.UI
{
    public partial class PauseScreen : MonoBehaviour
    {
        [SerializeField]
        private Player player;

        [SerializeField]
        private PlayerInput input;

        [SerializeField]
        private GameObject graphicsContainer;

        [SerializeField]
        [AutoEvent(nameof(CanvasButton.Clicked), nameof(OnContinueClicked))]
        private CanvasButton continueButton;

        [AutoEvent(nameof(IManagedInput.Performed), nameof(OnPauseInput))]
        private IManagedInput pauseInput;

        private bool isPaused = false;

        private void Awake()
        {
            pauseInput = input.PauseInput;
        }

        private void Update()
        {
            if (!isPaused)
                return;

            if (ManagedPointer.Current.Delta.sqrMagnitude > 0.0f)
            {
                CursorManager.SetCursorVisibility(true);
                CursorManager.SetCursorLockMode(CursorLockMode.None);
            }
        }

        private void OnPauseInput()
        {
            if (player.IsEnding)
                return;

            if (isPaused)
                Unpause();
            else
                Pause();
        }

        private void OnContinueClicked()
        {
            if (isPaused)
                Unpause();
        }

        private void Pause()
        {
            graphicsContainer.SetActive(true);
            PauseManager.Pause();

            isPaused = true;
        }

        private void Unpause()
        {
            graphicsContainer.SetActive(false);
            PauseManager.Unpause();

            CursorManager.SetCursorVisibility(false);
            CursorManager.SetCursorLockMode(CursorLockMode.Locked);

            isPaused = false;
        }
    }
}
