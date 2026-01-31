using UnityEngine;

namespace CultMask.Levels
{
    public class VisionElement : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private Renderer[] renderers;

        [SerializeField]
        private Collider[] colliders;

        [Header("Materials")]
        [SerializeField]
        private Material transparentMaterial;

        [SerializeField]
        private Material solidMaterial;

        [Header("Settings")]
        [SerializeField]
        private bool isSolidByDefault = false;

        private bool isApplicationQuitting = false;

        private void OnValidate()
        {
            if (!isSolidByDefault && transparentMaterial != null)
            {
                foreach (var renderer in renderers)
                    renderer.material = transparentMaterial;
            }
            else if (isSolidByDefault && solidMaterial != null)
            {
                foreach (var renderer in renderers)
                    renderer.material = solidMaterial;
            }
        }

        private void Awake()
        {
            VisionElementManager.Register(this);

            ReturnToDefault();
        }

        private void OnDestroy()
        {
            if (!isApplicationQuitting)
                VisionElementManager.Deregister(this);
        }

        private void OnApplicationQuit()
        {
            isApplicationQuitting = true;
        }

        public void Change()
        {
            foreach (var collider in colliders)
                collider.enabled = !isSolidByDefault;

            if (isSolidByDefault)
            {
                foreach (var renderer in renderers)
                    renderer.material = transparentMaterial;
            }
            else
            {
                foreach (var renderer in renderers)
                    renderer.material = solidMaterial;
            }
        }

        public void ReturnToDefault()
        {
            foreach (var collider in colliders)
                collider.enabled = isSolidByDefault;

            if (!isSolidByDefault)
            {
                foreach (var renderer in renderers)
                    renderer.material = transparentMaterial;
            }
            else
            {
                foreach (var renderer in renderers)
                    renderer.material = solidMaterial;
            }
        }
    }
}
