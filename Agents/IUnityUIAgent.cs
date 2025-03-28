using System;
using UnityEngine;

namespace Build1.UnityUI.Agents
{
    internal interface IUnityUIAgent
    {
        DeviceOrientation DeviceOrientation { get; }
        ScreenOrientation ScreenOrientation { get; }
        int               ScreenWidth       { get; }
        int               ScreenHeight      { get; }

        event Action             OnDeviceOrientationChanged;
        event Action<bool, bool> OnSomethingChanged;

        InterfaceType GetInterfaceType();
    }
}