using Shears;
using Shears.Cameras;
using UnityEngine;

namespace CultMask.Players
{
    [CustomWrapper(DisplayFields = new string[] { "states" })]
    [RequireComponent(typeof(ManagedCamera))]
    public class PlayerCamera : ManagedWrapper<ManagedCamera>
    {
        [SerializeField]
        private PlayerInput input;

        private ManagedCamera Camera => TypedWrappedValue;

        private void Awake()
        {
            Camera.Input = input.InputProvider;
            Camera.Initialize();
        }
    }
}
