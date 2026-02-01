using Shears.UI;
using UnityEngine;

namespace CultMask.UI
{
    public class Focuser : MonoBehaviour
    {
        [SerializeField]
        private UIElement element;

        private void Awake()
        {
            element.Focus();
        }
    }
}
