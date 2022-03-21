using System;
using UnityEngine;

namespace Build1.UnityUI.Agents
{
    internal interface IUnityUIAgent
    {
        ScreenOrientation DeviceOrientation { get; }
        int               ScreenWidth       { get; }
        int               ScreenHeight      { get; }

        event Action OnScreenResolutionChanged;
        event Action OnDeviceOrientationChanged;

        InterfaceType GetInterfaceType();
        void          Dispose();
    }
}