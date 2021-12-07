using DG.DemiLib.Attributes;
using UnityEngine;

namespace Development.Global.Code.NightAttributes
{
    [DeScriptExecutionOrder(-9000), DisallowMultipleComponent]
    public sealed class NightAttributesEntry : MonoBehaviour
    {
        private void Awake()
        {
            var all = FindObjectsOfType<MonoBehaviour>();

            foreach (var monoBehaviour in all)
                monoBehaviour.CheckForNightAttributes();
        }
    }
}