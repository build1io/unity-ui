using System;
using Build1.UnityUI.Utils;
using UnityEngine;

namespace Build1.UnityUI.Adaptive
{
    [Serializable]
    public sealed class InterfaceMarginSubItem
    {
        [SerializeField] public InterfaceType interfaceType;
        [SerializeField] public RectOffset    padding;

        /*
         * Static.
         */

        public static InterfaceMarginSubItem New(InterfaceType interfaceType)
        {
            var item = new InterfaceMarginSubItem
            {
                interfaceType = interfaceType,
                padding = new RectOffset()
            };
            return item;
        }
    }
}