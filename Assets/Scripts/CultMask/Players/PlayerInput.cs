using Shears.Input;
using UnityEngine;

namespace CultMask.Players
{
    [DefaultExecutionOrder(-1000)]
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField]
        private ManagedInputMap inputMap;

        public ManagedInputProvider InputProvider => inputMap;
        public IManagedInput MoveInput { get; private set; }
        public IManagedInput JumpInput { get; private set; }
        public IManagedInput DropFromLedgeInput { get; private set; }
        public IManagedInput DashInput { get; private set; }

        private void Awake()
        {
            inputMap.GetInputs(
                ("Move", i => MoveInput = i),
                ("Jump", i => JumpInput = i),
                ("Drop From Ledge", i => DropFromLedgeInput = i),
                ("Dash", i => DashInput = i)
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
