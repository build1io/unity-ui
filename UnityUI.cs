using System;
using Build1.UnityUI.Agents;
using UnityEngine;

namespace Build1.UnityUI
{
    public static class UnityUI
    {
        public static InterfaceType     CurrentInterfaceType { get; private set; }
        public static ScreenOrientation DeviceOrientation    => _agent.DeviceOrientation;
        public static int               ScreenWidth          => _agent.ScreenWidth;
        public static int               ScreenHeight         => _agent.ScreenHeight;

        public static event Action<InterfaceType> OnInterfaceTypeChanged;
        public static event Action                OnDeviceOrientationChanged;
        public static event Action                OnScreenResolutionChanged;
        public static event Action                OnSomethingChanged;

        private static readonly IUnityUIAgent _agent;

        static UnityUI()
        {
            #if UNITY_EDITOR
            
            _agent = InitializeAgent(UnityUIAgentEditor.Create());
            
            #else
            
            var root = new GameObject("[UnityUI]");
            UnityEngine.Object.DontDestroyOnLoad(root);    
            
            _agent = InitializeAgent(UnityUIAgentRuntime.Create(root));
            
            #endif

            CurrentInterfaceType = _agent.GetInterfaceType();
        }

        /*
         * Public.
         */
        
        public static float GetScreenAspectRatio()
        {
            return Mathf.Max((float)_agent.ScreenWidth, _agent.ScreenHeight) / Mathf.Min(_agent.ScreenWidth, _agent.ScreenHeight);
        }

        public static float GetScreenDiagonalInches()
        {
            var screenWidth = _agent.ScreenWidth / Screen.dpi;
            var screenHeight = _agent.ScreenHeight / Screen.dpi;
            return Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));
        }

        internal static bool IsTablet()
        {
            var diagonal = GetScreenDiagonalInches();
            var aspect = GetScreenAspectRatio();
            if (aspect >= 1.77)
                return diagonal >= 7F;
            return diagonal >= 5F;
        }

        /*
         * Private.
         */

        private static IUnityUIAgent InitializeAgent(IUnityUIAgent agent)
        {
            agent.OnSomethingChanged += OnSomethingChangedHandler;
            return agent;
        }

        /*
         * Event Handlers.
         */

        private static void OnSomethingChangedHandler(bool deviceOrientationChanged, bool screenResolutionChanged)
        {
            var interfaceTypeChanged = false;

            if (screenResolutionChanged)
            {
                var interfaceType = _agent.GetInterfaceType();
                if (interfaceType != CurrentInterfaceType)
                {
                    CurrentInterfaceType = interfaceType;
                    interfaceTypeChanged = true;
                }
            }

            if (deviceOrientationChanged)
                OnDeviceOrientationChanged?.Invoke();

            if (screenResolutionChanged)
                OnScreenResolutionChanged?.Invoke();

            if (interfaceTypeChanged)
                OnInterfaceTypeChanged?.Invoke(CurrentInterfaceType);

            if (deviceOrientationChanged || screenResolutionChanged)
                OnSomethingChanged?.Invoke();
        }
    }
}