using UnityEngine;
using UnityEngine.TextCore.Text;

namespace CultMask.Players
{
    [RequireComponent(typeof(PlayerStateMachine), typeof(PlayerController))]
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField]
        private PlayerCharacterData data;

        [SerializeField]
        private PlayerStateFlags stateFlags;

        private Player player;
        private PlayerInput input;
        new private PlayerCamera camera;
        private PlayerStateMachine stateMachine;
        private PlayerController controller;

        public Player Player => player;
        public PlayerCharacterData Data => data;
        public PlayerInput Input => input;
        public PlayerCamera Camera => camera;
        public PlayerStateMachine StateMachine => stateMachine;
        public PlayerStateFlags StateFlags => stateFlags;
        public PlayerController Controller => controller;

        public static PlayerCharacter Spawn(PlayerCharacter prefab, Player player, PlayerCamera camera)
        {
            var character = Instantiate(prefab, player.transform);

            character.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            character.Spawn(player, camera);

            return character;
        }

        public void Spawn(Player player, PlayerCamera camera)
        {
            this.player = player;
            input = player.Input;
            this.camera = camera;
            stateMachine = GetComponent<PlayerStateMachine>();
            controller = GetComponent<PlayerController>();

            stateFlags = new(this);

            stateMachine.InitializeStates();
        }

        private void Update()
        {
            stateFlags.Update();
        }
    }
}
