using Shears.Input;
using UnityEngine;

namespace CultMask.Players
{
    [DefaultExecutionOrder(-1000)]
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField]
        private ManagedInputMap inputMap;

        private IManagedInput moveInput;
        private IManagedInput jumpInput;

        public ManagedInputProvider InputProvider => inputMap;
        public IManagedInput MoveInput => moveInput;
        public IManagedInput JumpInput => jumpInput;

        private void Awake()
        {
            inputMap.GetInputs(
                ("Move", i => moveInput = i),
                ("Jump", i => jumpInput = i)
            );
        }

        public void Enable()
        {
            inputMap.EnableAllInputs();
        }

        public void Disable()
        {
            inputMap.DisableAllInputs();
        }
    }
}
