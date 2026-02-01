using Shears;
using System;
using System.Collections;
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

        [SerializeField]
        private PlayerUnlocks unlocks;

        [Header("Respawning")]
        [SerializeField]
        private Transform respawnPoint;

        [SerializeField]
        private float respawnTime = 5.0f;

        private PlayerInput input;

        public PlayerCharacter Character => characterInstance;
        public PlayerInput Input => input;
        public PlayerUnlocks Unlocks => unlocks;

        public bool IsEnding { get; set; } = false;

        public event Action<PlayerCharacter> CharacterSpawned;

        private void Awake()
        {
            input = GetComponent<PlayerInput>();
        }

        private void Start()
        {
            SpawnCharacter();

            input.Enable();
        }

        public void UnlockAbility(Levels.AbilityData data) => unlocks.UnlockAbility(data);

        private void SpawnCharacter()
        {
            if (characterInstance != null)
                characterInstance.Spawn(this, camera);
            else
                characterInstance = PlayerCharacter.Spawn(characterPrefab, this, camera);

            camera.SetTarget(characterInstance.transform);
            characterInstance.Died += OnCharacterDied;

            characterInstance.Controller.SetPosition(respawnPoint.position);

            CharacterSpawned?.Invoke(characterInstance);
        }

        private void OnCharacterDied()
        {
            characterInstance = null;
            Respawn();
        }

        private void Respawn()
        {
            StartCoroutine(IERespawn());
        }

        private IEnumerator IERespawn()
        {
            yield return CoroutineUtil.WaitForSeconds(respawnTime);

            SpawnCharacter();
        }
    }
}
