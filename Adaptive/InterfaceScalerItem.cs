using System;
using System.Linq;
using Build1.UnityUI.Utils;
using UnityEngine;

namespace Build1.UnityUI.Adaptive
{
    [Serializable]
    public sealed class InterfaceScalerItem
    {
        [SerializeField] public GameObject               gameObject;
        [SerializeField] public InterfaceScalerSubItem[] items;
        
        /*
         * Static.
         */

        public static InterfaceScalerItem New(GameObject gameObject)
        {
            var item = new InterfaceScalerItem
            {
                gameObject = gameObject
            };

            var interfaceTypes = Enum.GetValues(typeof(InterfaceType)).Cast<InterfaceType>();
            item.items = interfaceTypes.Select(InterfaceScalerSubItem.New).ToArray();
            return item;
        }
    }
}