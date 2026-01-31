using UnityEngine;

namespace CultMask.Players
{
    [RequireComponent(typeof(PlayerInput), typeof(PlayerStateMachine), typeof(PlayerController))]
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private PlayerData data;

        [SerializeField]
        new private PlayerCamera camera;

        [SerializeField]
        private PlayerStateFlags stateFlags;

        private PlayerInput input;
        private PlayerStateMachine stateMachine;
        private PlayerController controller;

        public PlayerData Data => data;
        public PlayerInput Input => input;
        public PlayerCamera Camera => camera;
        public PlayerController Controller => controller;
        public PlayerStateFlags StateFlags => stateFlags;

        private void Awake()
        {
            input = GetComponent<PlayerInput>();
            stateMachine = GetComponent<PlayerStateMachine>();
            controller = GetComponent<PlayerController>();

            stateFlags = new(this);
        }

        private void Start()
        {
            input.Enable();
            stateMachine.InitializeStates();
        }
    }
}
