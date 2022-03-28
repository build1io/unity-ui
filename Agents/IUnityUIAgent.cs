using System;
using UnityEngine;

namespace Build1.UnityUI.Agents
{
    internal interface IUnityUIAgent
    {
        ScreenOrientation DeviceOrientation { get; }
        int               ScreenWidth       { get; }
        int               ScreenHeight      { get; }

        event Action<bool, bool> OnSomethingChanged;

        InterfaceType GetInterfaceType();
        void          Dispose();
    }
}