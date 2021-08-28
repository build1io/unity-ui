using System;
using System.Linq;
using Build1.UnityUI.Utils;
using UnityEngine;

namespace Build1.UnityUI.Adaptive
{
    [Serializable]
    public sealed class InterfaceMarginItem
    {
        [SerializeField] public RectTransform            rectTransform;
        [SerializeField] public InterfaceMarginSubItem[] items;

        /*
         * Static.
         */

        public static InterfaceMarginItem New(RectTransform rectTransform)
        {
            var item = new InterfaceMarginItem
            {
                rectTransform = rectTransform
            };

            var interfaceTypes = Enum.GetValues(typeof(InterfaceType)).Cast<InterfaceType>();
            item.items = interfaceTypes.Select(InterfaceMarginSubItem.New).ToArray();
            return item;
        }
    }
}