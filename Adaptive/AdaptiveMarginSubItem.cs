using System;
using UnityEngine;

namespace Build1.UnityUI.Adaptive
{
    [Serializable]
    public sealed class AdaptiveMarginSubItem
    {
        [SerializeField] public InterfaceType interfaceType;
        [SerializeField] public RectOffset    padding;

        /*
         * Static.
         */

        public static AdaptiveMarginSubItem New(InterfaceType interfaceType, RectTransform rectTransform)
        {
            return new AdaptiveMarginSubItem
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