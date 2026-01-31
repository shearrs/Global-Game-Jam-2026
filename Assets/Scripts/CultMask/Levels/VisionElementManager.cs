using Shears;
using System.Collections.Generic;
using UnityEngine;

namespace CultMask.Levels
{
    public class VisionElementManager : ProtectedSingleton<VisionElementManager>
    {
        private readonly HashSet<VisionElement> elements = new();

        public static void Register(VisionElement element) => Instance.InstRegister(element);
        private void InstRegister(VisionElement element)
        {
            if (!elements.Contains(element))
                elements.Add(element);
        }

        public static void Deregister(VisionElement element) => Instance.InstDeregister(element);
        private void InstDeregister(VisionElement element)
        {
            elements.Remove(element);
        }

        public static void EnableVision() => Instance.InstEnableVision();
        private void InstEnableVision()
        {
            foreach (var element in elements)
                element.Change();
        }

        public static void DisableVision() => Instance.InstDisableVision();
        private void InstDisableVision()
        {
            foreach (var element in elements)
                element.ReturnToDefault();
        }
    }
}
