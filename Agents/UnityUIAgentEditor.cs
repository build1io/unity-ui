#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityUI.Agents
{
    internal sealed class UnityUIAgentEditor : IUnityUIAgent
    {
        public ScreenOrientation ScreenOrientation { get; private set; }
        public int               ScreenWidth       { get; private set; }
        public int               ScreenHeight      { get; private set; }

        public event Action<bool, bool> OnSomethingChanged;

        public UnityUIAgentEditor()
        {
            ScreenWidth = Screen.currentResolution.width;
            ScreenHeight = Screen.currentResolution.height;

            EditorApplication.update += OnUpdate;
        }

        /*
         * Public.
         */

        public InterfaceType GetInterfaceType()
        {
            return GetInterfaceTypeStatic();
        }

        public void Dispose()
        {
            EditorApplication.update -= OnUpdate;

            OnSomethingChanged = null;
        }

        /*
         * Private.
         */

        private void OnUpdate()
        {
            var screenOrientationChanged = false;
            var screenResolutionChanged = false;

            if (ScreenOrientation != Screen.orientation)
            {
                ScreenOrientation = Screen.orientation;
                screenOrientationChanged = true;
            }

            if (ScreenWidth != Screen.currentResolution.width || ScreenHeight != Screen.currentResolution.height)
            {
                ScreenWidth = Screen.currentResolution.width;
                ScreenHeight = Screen.currentResolution.height;

                screenResolutionChanged = true;
            }

            if (screenOrientationChanged || screenResolutionChanged)
                OnSomethingChanged?.Invoke(screenOrientationChanged, screenResolutionChanged);
        }

        /*
         * Static.
         */

        public static IUnityUIAgent Create()
        {
            return new UnityUIAgentEditor();
        }

        public static InterfaceType GetInterfaceTypeStatic()
        {
            var target = EditorUserBuildSettings.activeBuildTarget;
            switch (target)
            {
                case BuildTarget.iOS:
                case BuildTarget.Android:
                    return UnityUI.IsTablet() ? InterfaceType.Tablet : InterfaceType.Phone;

                case BuildTarget.StandaloneOSX:
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                case BuildTarget.StandaloneLinux64:
                    return InterfaceType.Desktop;

                case BuildTarget.XboxOne:
                case BuildTarget.GameCoreXboxOne:
                case BuildTarget.PS4:
                case BuildTarget.PS5:
                case BuildTarget.Switch:
                    return InterfaceType.Console;

                case BuildTarget.WebGL:
                    return InterfaceType.Web;

                case BuildTarget.tvOS:
                    return InterfaceType.TV;

                default:
                    throw new ArgumentOutOfRangeException($"Unknown build type. Target: {target}");
            }
        }
    }
}

#endif