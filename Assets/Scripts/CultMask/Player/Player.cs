using Shears;
using UnityEngine;

namespace CultMask.Players
{
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField]
        private PlayerCharacter characterPrefab;

        [Header("Instances")]
        [SerializeField]
        new private PlayerCamera camera;

        [SerializeField, RuntimeReadOnly]
        private PlayerCharacter characterInstance;

        private PlayerInput input;

        public PlayerCharacter Character => characterInstance;
        public PlayerInput Input => input;

        private void Awake()
        {
            input = GetComponent<PlayerInput>();

            SpawnCharacter();
        }

        private void Start()
        {
            input.Enable();
        }

        private void SpawnCharacter()
        {
            if (characterInstance != null)
                characterInstance.Spawn(this, camera);
            else
                characterInstance = PlayerCharacter.Spawn(characterPrefab, this, camera);
        }
    }
}
