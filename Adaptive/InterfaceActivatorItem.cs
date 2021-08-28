using System;
using Build1.UnityUI.Utils;
using UnityEngine;

namespace Build1.UnityUI.Adaptive
{
    [Serializable]
    public sealed class InterfaceActivatorItem
    {
        [SerializeField] public GameObject    gameObject;
        [SerializeField] public InterfaceType interfaceType;
        
        /*
         * Static.
         */

        public static InterfaceActivatorItem New(GameObject gameObject)
        {
            var item = new InterfaceActivatorItem
            {
                gameObject = gameObject
            };
            return item;
        }
    }
}