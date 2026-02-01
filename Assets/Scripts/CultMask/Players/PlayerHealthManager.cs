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

        [AutoEvent(nameof(Timer.Completed), nameof(OnRegenTimerCompleted))]
        private readonly Timer regenTimer = new(10.0f);

        private PlayerCharacter character;
        private PlayerCharacterData data;

        public int Health => health;

        public event Action<int> HealthChanged;
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

            ChangeHealth(-1);
            
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

        private void OnRegenTimerCompleted()
        {
            ChangeHealth(1);
        }

        private void ChangeHealth(int change)
        {
            health = Mathf.Clamp(health + change, 0, data.MaxHealth);

            HealthChanged?.Invoke(health);

            regenTimer.Restart();
        }
    }
}
