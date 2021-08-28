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

        public static InterfaceMarginSubItem New(InterfaceType interfaceType, RectTransform rectTransform)
        {
            return new InterfaceMarginSubItem
            {
                interfaceType = interfaceType,
                padding = rectTransform == null
                              ? new RectOffset()
                              : new RectOffset((int)rectTransform.offsetMin.x,
                                               -(int)rectTransform.offsetMax.x,
                                               -(int)rectTransform.offsetMax.y,
                                               (int)rectTransform.offsetMin.y),
            };
        }
    }
}