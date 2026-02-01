using Shears;
using Shears.HitDetection;
using UnityEngine;

namespace CultMask.Enemies
{
    [RequireComponent(typeof(HitBody3D))]
    public partial class EnemyContactDamage : MonoBehaviour
    {
        [Auto]
        [AutoEvent(nameof(HitBody3D.HitDelivered), nameof(OnHitDelivered))]
        private HitBody3D hitBody;

        [AutoEvent(nameof(Timer.Completed), nameof(OnHitDelayCompleted))]
        private readonly Timer hitDelayTimer = new(1.0f);

        private void OnHitDelivered(HitData3D _)
        {
            hitBody.Disable();
            hitDelayTimer.Restart();
        }

        private void OnHitDelayCompleted()
        {
            hitBody.Enable();
        }
    }
}
