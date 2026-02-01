using CultMask.Levels;
using CultMask.UI;
using Shears;
using UnityEngine;

namespace CultMask.Players.Graphics
{
    [RequireComponent(typeof(PlayerAbilityManager))]
    public partial class PlayerUnlockGraphics : MonoBehaviour
    {
        [SerializeField]
        private PlayerCharacter character;

        [SerializeField]
        [AutoEvent(nameof(Popup.Closed), nameof(OnPopupClosed))]
        private Popup popup;

        [Auto]
        [AutoEvent(nameof(PlayerAbilityManager.AbilityUnlocked), nameof(OnAbilityUnlocked))]
        private PlayerAbilityManager abilityManager;

        private void OnAbilityUnlocked(AbilityUnlock unlock)
        {
            PauseManager.Pause();

            popup.Title = unlock.Data.Title;
            popup.Text = unlock.Data.Description;
            popup.Open();
        }

        private void OnPopupClosed()
        {
            PauseManager.Unpause();
        }
    }
}
