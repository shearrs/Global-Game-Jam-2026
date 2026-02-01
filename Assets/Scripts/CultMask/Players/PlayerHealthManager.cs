using Shears;
using Shears.HitDetection;
using UnityEngine;

namespace CultMask.Players
{
    public partial class PlayerHealthManager : MonoBehaviour
    {
        [SerializeField, Min(0)]
        private int health = 4;

        [SerializeField]
        [AutoEvent(nameof(HurtBody3D.HitReceived), nameof(OnHitReceived))]
        private HurtBody3D hurtBody;

        private PlayerCharacter character;
        private PlayerCharacterData data;

        public void Initialize(PlayerCharacter character)
        {
            this.character = character;
            data = character.Data;

            health = data.MaxHealth;
        }

        private void OnHitReceived(HitData3D data)
        {
            health = Mathf.Max(0, health - 1);

            if (health == 0)
                character.Die();
        }
    }
}
