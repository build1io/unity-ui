using System;
using Build1.UnityUI.Utils;
using UnityEngine;

namespace Build1.UnityUI.Adaptive
{
    [Serializable]
    public sealed class InterfaceScalerSubItem
    {
        [SerializeField] public InterfaceType interfaceType;
        [SerializeField] public float         scale = 1;
        
        /*
         * Static.
         */

        public static InterfaceScalerSubItem New(InterfaceType interfaceType)
        {
            var item = new InterfaceScalerSubItem
            {
                interfaceType = interfaceType
            };
            return item;
        }
    }
}