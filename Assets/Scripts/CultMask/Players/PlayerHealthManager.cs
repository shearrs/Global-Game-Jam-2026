using Shears;
using Shears.HitDetection;
using System;
using UnityEngine;

namespace CultMask.Players
{
    public partial class PlayerHealthManager : MonoBehaviour
    {
        [SerializeField, Min(0)]
        private int health = 4;

        [SerializeField]
        private float knockbackForce = 16.0f;

        [SerializeField]
        private Timer invulnerabilityTimer = new(1.0f);

        [SerializeField]
        [AutoEvent(nameof(HurtBody3D.HitReceived), nameof(OnHitReceived))]
        private HurtBody3D hurtBody;

        private PlayerCharacter character;
        private PlayerCharacterData data;

        public event Action<HitData3D> DamageReceived;

        public void Initialize(PlayerCharacter character)
        {
            this.character = character;
            data = character.Data;

            health = data.MaxHealth;
        }

        private void OnHitReceived(HitData3D data)
        {
            if (!invulnerabilityTimer.IsDone)
                return;

            health = Mathf.Max(0, health - 1);
            
            if (health == 0)
                character.Die();
            else
            {
                Vector3 direction = (character.transform.position - data.HitBody.transform.position).With(y: .5f).normalized;
                character.Controller.AddVelocity(direction * knockbackForce);

                invulnerabilityTimer.Restart();
                DamageReceived?.Invoke(data);
            }
        }
    }
}
