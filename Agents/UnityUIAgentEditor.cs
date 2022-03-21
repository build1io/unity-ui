#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityUI.Agents
{
    internal sealed class UnityUIAgentEditor : IUnityUIAgent
    {
        public ScreenOrientation DeviceOrientation => Screen.orientation;
        
        public int ScreenWidth  { get; private set; }
        public int ScreenHeight { get; private set; }

        public event Action OnScreenResolutionChanged;
        
        #pragma warning disable 414
        
        public event Action OnDeviceOrientationChanged;
        
        #pragma warning restore 414

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
            return GetInterfaceTypeStatic(this);
        }

        public void Dispose()
        {
            EditorApplication.update -= OnUpdate;
            
            OnScreenResolutionChanged = null;
            OnDeviceOrientationChanged = null;
        }
        
        /*
         * Private.
         */

        private void OnUpdate()
        {
            if (ScreenWidth != Screen.currentResolution.width || ScreenHeight != Screen.currentResolution.height)
            {
                ScreenWidth = Screen.currentResolution.width;
                ScreenHeight = Screen.currentResolution.height;

                OnScreenResolutionChanged?.Invoke();
            }
        }
        
        /*
         * Static.
         */

        public static IUnityUIAgent Create()
        {
            return new UnityUIAgentEditor();
        }

        public static InterfaceType GetInterfaceTypeStatic(IUnityUIAgent agent)
        {
            var target = EditorUserBuildSettings.activeBuildTarget;
            switch (target)
            {
                case BuildTarget.iOS:
                case BuildTarget.Android:
                    return UnityUI.IsTablet(agent) ? InterfaceType.Tablet : InterfaceType.Phone;
                
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