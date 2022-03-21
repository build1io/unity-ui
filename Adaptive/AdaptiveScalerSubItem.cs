using System;
using UnityEngine;

namespace Build1.UnityUI.Adaptive
{
    [Serializable]
    public sealed class AdaptiveScalerSubItem
    {
        [SerializeField] public InterfaceType interfaceType;
        [SerializeField] public float         scale = 1;
        
        /*
         * Static.
         */

        public static AdaptiveScalerSubItem New(InterfaceType interfaceType)
        {
            var item = new AdaptiveScalerSubItem
            {
                interfaceType = interfaceType
            };
            return item;
        }
    }
}