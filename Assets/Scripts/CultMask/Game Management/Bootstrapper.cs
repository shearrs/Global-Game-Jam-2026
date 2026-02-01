using Shears;
using Shears.Loading;
using UnityEngine;

namespace CultMask.GameManagement
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField]
        private LoadRequest initialLoadRequest;

        private void Awake()
        {
            Loader.EnqueueRequest(initialLoadRequest);

            Loader.LoadCompleted += Dispose;
        }

        private void Dispose()
        {
            Loader.LoadCompleted -= Dispose;

            Destroy(gameObject);
        }
    }
}
