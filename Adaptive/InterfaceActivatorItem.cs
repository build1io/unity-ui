using System;
using UnityEngine;

namespace Build1.UnityUI.Adaptive
{
    [Serializable]
    public sealed class InterfaceActivatorItem
    {
        [SerializeField] public GameObject    gameObject;
        [SerializeField] public InterfaceType interfaceType;
    }
}