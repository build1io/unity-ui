using System;
using System.Linq;
using Build1.UnityUI.Utils;
using UnityEngine;

namespace Build1.UnityUI.Adaptive
{
    [Serializable]
    public sealed class AdaptiveMarginItem
    {
        [SerializeField] public RectTransform            rectTransform;
        [SerializeField] public AdaptiveMarginSubItem[] items;

        /*
         * Static.
         */

        public static AdaptiveMarginItem New(RectTransform rectTransform)
        {
            var item = new AdaptiveMarginItem
            {
                rectTransform = rectTransform
            };

            var interfaceTypes = Enum.GetValues(typeof(InterfaceType)).Cast<InterfaceType>();
            item.items = interfaceTypes.Select(i => AdaptiveMarginSubItem.New(i, rectTransform)).ToArray();
            return item;
        }
    }
}