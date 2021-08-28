using System;
using System.Linq;
using Build1.UnityUI.Utils;
using UnityEngine;

namespace Build1.UnityUI.Adaptive
{
    [Serializable]
    public sealed class AdaptiveScalerItem
    {
        [SerializeField] public GameObject               gameObject;
        [SerializeField] public AdaptiveScalerSubItem[] items;
        
        /*
         * Static.
         */

        public static AdaptiveScalerItem New(GameObject gameObject)
        {
            var item = new AdaptiveScalerItem
            {
                gameObject = gameObject
            };

            var interfaceTypes = Enum.GetValues(typeof(InterfaceType)).Cast<InterfaceType>();
            item.items = interfaceTypes.Select(AdaptiveScalerSubItem.New).ToArray();
            return item;
        }
    }
}