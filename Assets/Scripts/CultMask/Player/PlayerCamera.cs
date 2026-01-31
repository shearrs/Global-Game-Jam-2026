using Shears;
using Shears.Cameras;
using UnityEngine;

namespace CultMask.Players
{
    [CustomWrapper(ShowAllFields = true)]
    [RequireComponent(typeof(ManagedCamera))]
    public class PlayerCamera : ManagedWrapper<ManagedCamera>
    {
        
    }
}
