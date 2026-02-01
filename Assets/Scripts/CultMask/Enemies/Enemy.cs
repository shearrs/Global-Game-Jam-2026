using Shears.Detection;
using Shears.HitDetection;
using UnityEngine;

namespace CultMask.Enemies
{
    [RequireComponent(typeof(EnemyStateMachine), typeof(EnemyStateFlags), typeof(EnemyController))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private EnemyData data;

        [SerializeField]
        private AreaDetector3D targetDetector;

        [SerializeField]
        private RayDetector3D lineOfSightDetector;

        [SerializeField]
        private HitBody3D hitBody;

        private EnemyStateMachine stateMachine;
        private EnemyStateFlags stateFlags;
        private EnemyController controller;

        public EnemyData Data => data;
        public EnemyStateFlags StateFlags => stateFlags;
        public EnemyController Controller => controller;
        public AreaDetector3D TargetDetector => targetDetector;
        public RayDetector3D LineOfSightDetector => lineOfSightDetector;
        public HitBody3D HitBody => hitBody;

        private void Awake()
        {
            stateMachine = GetComponent<EnemyStateMachine>();
            stateFlags = GetComponent<EnemyStateFlags>();
            controller = GetComponent<EnemyController>();

            stateFlags.Initialize(this);
        }

        private void Start()
        {
            stateMachine.InitializeStates();
        }

        private void Update()
        {
            stateFlags.UpdateFlags();
        }
    }
}
