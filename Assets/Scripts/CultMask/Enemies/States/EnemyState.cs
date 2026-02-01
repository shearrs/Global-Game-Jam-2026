using Shears.StateMachineGraphs;
using UnityEngine;

namespace CultMask.Enemies
{
    public abstract class EnemyState : State
    {
        private Enemy enemy;
        private EnemyStateFlags flags;

        protected Enemy Enemy => enemy;
        protected EnemyData Data => enemy.Data;
        protected EnemyStateFlags Flags => flags;
        protected EnemyController Controller => enemy.Controller;

        public void Initialize(Enemy enemy)
        {
            this.enemy = enemy;
            flags = enemy.StateFlags;
        }
    }
}
