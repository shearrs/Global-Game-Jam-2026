using Shears;
using Shears.Cameras;
using UnityEngine;

namespace CultMask.Players
{
    [CustomWrapper(DisplayFields = new string[] { "states" })]
    [RequireComponent(typeof(ManagedCamera))]
    public partial class PlayerCamera : ManagedWrapper<ManagedCamera>
    {
        [SerializeField]
        private PlayerInput input;

        [Auto]
        private ThirdPersonCameraState state;

        private ManagedCamera Camera => TypedWrappedValue;

        private void Awake()
        {
            __AutoAwake();

            Camera.Input = input.InputProvider;
            Camera.Initialize();

            state.Zoom = state.MaxZoom;
        }
    }
}
