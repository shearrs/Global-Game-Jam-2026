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

        [SerializeField]
        private PlayerUnlocks unlocks;

        [SerializeField, RuntimeReadOnly]
        private PlayerCharacter characterInstance;

        private PlayerInput input;

        public PlayerCharacter Character => characterInstance;
        public PlayerInput Input => input;
        public PlayerUnlocks Unlocks => unlocks;

        private void Awake()
        {
            input = GetComponent<PlayerInput>();

            SpawnCharacter();
        }

        private void Start()
        {
            input.Enable();
        }

        [ContextMenu("Respawn Character")]
        private void RespawnCharacter()
        {
            Destroy(characterInstance.gameObject);
            characterInstance = null;

            SpawnCharacter();
        }

        public void UnlockAbility(Levels.AbilityData data) => unlocks.UnlockAbility(data);

        private void SpawnCharacter()
        {
            if (characterInstance != null)
                characterInstance.Spawn(this, camera);
            else
                characterInstance = PlayerCharacter.Spawn(characterPrefab, this, camera);

            camera.SetTarget(characterInstance.transform);
        }
    }
}
