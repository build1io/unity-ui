using System;
using UnityEngine;

namespace Build1.UnityUI.Agents
{
    [DisallowMultipleComponent]
    internal class UnityUIAgentRuntime : MonoBehaviour, IUnityUIAgent
    {
        public ScreenOrientation DeviceOrientation { get; private set; }
        public int               ScreenWidth       { get; private set; }
        public int               ScreenHeight      { get; private set; }

        public event Action<bool, bool> OnSomethingChanged;

        private void Awake()
        {
            DeviceOrientation = Screen.orientation;
            ScreenWidth = Screen.width;
            ScreenHeight = Screen.height;
        }

        private void Update()
        {
            var deviceOrientationChanged = false;
            var screenResolutionChanged = false;
            var safeAreaChanged = false;
            
            if (DeviceOrientation != Screen.orientation)
            {
                DeviceOrientation = Screen.orientation;
                deviceOrientationChanged = true;
            }

            if (ScreenWidth != Screen.width || ScreenHeight != Screen.height)
            {
                ScreenWidth = Screen.width;
                ScreenHeight = Screen.height;
                screenResolutionChanged = true;
            }
            
            if (deviceOrientationChanged || screenResolutionChanged || safeAreaChanged)
                OnSomethingChanged?.Invoke(deviceOrientationChanged, screenResolutionChanged);
        }

        /*
         * Public.
         */

        public virtual InterfaceType GetInterfaceType()
        {
            var platform = Application.platform;
            var deviceType = SystemInfo.deviceType;

            switch (platform)
            {
                case RuntimePlatform.IPhonePlayer:
                    return UnityUI.IsTablet(this) ? InterfaceType.Tablet : InterfaceType.Phone;

                case RuntimePlatform.Android:
                    return deviceType switch
                    {
                        DeviceType.Handheld => UnityUI.IsTablet(this) ? InterfaceType.Tablet : InterfaceType.Phone,
                        DeviceType.Console  => InterfaceType.Console,
                        DeviceType.Desktop  => InterfaceType.Desktop,
                        _                   => throw new ArgumentOutOfRangeException($"Unknown interface type. Platform: {platform} DeviceType: {deviceType}")
                    };

                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.LinuxEditor:
                case RuntimePlatform.OSXPlayer:
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.LinuxPlayer:
                    return InterfaceType.Desktop;

                case RuntimePlatform.WebGLPlayer:
                    return InterfaceType.Web;

                case RuntimePlatform.PS4:
                case RuntimePlatform.PS5:
                case RuntimePlatform.XboxOne:
                case RuntimePlatform.Switch:
                    return InterfaceType.Console;

                case RuntimePlatform.tvOS:
                    return InterfaceType.TV;

                default:
                    throw new ArgumentOutOfRangeException($"Unknown interface type. Platform: {platform} DeviceType: {deviceType}");
            }
        }

        public void Dispose()
        {
            OnSomethingChanged = null;
        }

        /*
         * Static.
         */

        public static IUnityUIAgent Create(GameObject root)
        {
            return root.AddComponent<UnityUIAgentRuntime>();
        }
    }
}