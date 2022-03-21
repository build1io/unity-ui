using System;
using UnityEngine;

namespace Build1.UnityUI.Adaptive
{
    [Serializable]
    public sealed class AdaptiveActivatorItem
    {
        [SerializeField] public GameObject    gameObject;
        [SerializeField] public InterfaceType interfaceType;
        
        /*
         * Static.
         */

        public static AdaptiveActivatorItem New(GameObject gameObject)
        {
            var item = new AdaptiveActivatorItem
            {
                gameObject = gameObject
            };
            return item;
        }
    }
}