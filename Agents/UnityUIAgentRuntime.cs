using System;
using UnityEngine;

namespace Build1.UnityUI.Agents
{
    [DisallowMultipleComponent]
    internal sealed class UnityUIAgentRuntime : MonoBehaviour, IUnityUIAgent
    {
        public DeviceOrientation DeviceOrientation { get; private set; }
        public ScreenOrientation ScreenOrientation { get; private set; }
        public int               ScreenWidth       { get; private set; }
        public int               ScreenHeight      { get; private set; }

        public event Action             OnDeviceOrientationChanged;
        public event Action<bool, bool> OnSomethingChanged;

        private void Awake()
        {
            DeviceOrientation = Input.deviceOrientation;
            ScreenOrientation = Screen.orientation;
            ScreenWidth = Screen.width;
            ScreenHeight = Screen.height;
        }

        private void Update()
        {
            var screenOrientationChanged = false;
            var screenResolutionChanged = false;
            var safeAreaChanged = false;

            if (DeviceOrientation != Input.deviceOrientation)
            {
                DeviceOrientation = Input.deviceOrientation;
                OnDeviceOrientationChanged?.Invoke();
            }

            if (ScreenOrientation != Screen.orientation)
            {
                ScreenOrientation = Screen.orientation;
                screenOrientationChanged = true;
            }

            if (ScreenWidth != Screen.width || ScreenHeight != Screen.height)
            {
                ScreenWidth = Screen.width;
                ScreenHeight = Screen.height;
                screenResolutionChanged = true;
            }

            if (screenOrientationChanged || screenResolutionChanged || safeAreaChanged)
                OnSomethingChanged?.Invoke(screenOrientationChanged, screenResolutionChanged);
        }

        /*
         * Public.
         */

        public InterfaceType GetInterfaceType()
        {
            var platform = Application.platform;
            var deviceType = SystemInfo.deviceType;

            switch (platform)
            {
                case RuntimePlatform.IPhonePlayer:
                    return UnityUI.IsTablet() ? InterfaceType.Tablet : InterfaceType.Phone;

                case RuntimePlatform.Android:
                    return deviceType switch
                    {
                        DeviceType.Handheld => UnityUI.IsTablet() ? InterfaceType.Tablet : InterfaceType.Phone,
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

        /*
         * Static.
         */

        public static IUnityUIAgent Create(GameObject root)
        {
            return root.AddComponent<UnityUIAgentRuntime>();
        }
    }
}