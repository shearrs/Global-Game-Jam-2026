using Shears;
using Shears.HitDetection;
using UnityEngine;

namespace CultMask.Enemies
{
    [RequireComponent(typeof(HurtBody3D))]
    public partial class EnemyHealthManager : MonoBehaviour
    {
        [SerializeField]
        private Enemy enemy;

        [Auto]
        [AutoEvent(nameof(HurtBody3D.HitReceived), nameof(OnHitReceived))]
        private HurtBody3D hurtBody;

        private int health = 3;

        private void OnHitReceived(HitData3D _)
        {
            health--;

            if (health <= 0)
                enemy.Die();
        }
    }
}
