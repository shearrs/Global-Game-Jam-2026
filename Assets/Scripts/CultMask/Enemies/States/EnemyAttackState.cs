using Shears;
using Shears.HitDetection;
using UnityEngine;

namespace CultMask.Enemies
{
    [System.Serializable]
    public class EnemyAttackState : EnemyState
    {
        private readonly Timer attackDelayTimer = new();
        private readonly Timer attackActiveTimer = new();

        private HitBody3D HitBody => Enemy.HitBody;

        public bool IsDoneAttacking { get; private set; } = false;

        public EnemyAttackState()
        {
            Name = "Attack";
        }

        protected override void OnEnter()
        {
            IsDoneAttacking = false;
            attackDelayTimer.Restart(Data.AttackDelay);

            attackDelayTimer.Completed += BeginAttack;
            attackActiveTimer.Completed += EndAttack;

            Controller.SetDestination(Controller.transform.position);
            Controller.ToggleRotation(false);

            Vector3 direction = (Flags.Target.position - Enemy.transform.position).With(y: 0.0f);

            Enemy.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }

        protected override void OnExit()
        {
            attackDelayTimer.Completed -= BeginAttack;
            attackActiveTimer.Completed -= EndAttack;

            Controller.ToggleRotation(true);
        }

        protected override void OnUpdate()
        {
        }

        private void BeginAttack()
        {
            HitBody.Enable();

            attackActiveTimer.Start(Data.AttackDuration);
        }

        private void EndAttack()
        {
            HitBody.Disable();
            IsDoneAttacking = true;
        }
    }
}
