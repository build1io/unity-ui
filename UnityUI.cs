using System;
using Build1.UnityUI.Agents;
using UnityEngine;
using Object = UnityEngine.Object;

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

        private static          GameObject    _root;
        private static readonly IUnityUIAgent _agent;

        static UnityUI()
        {
            #if UNITY_EDITOR
            _agent = InitializeAgent(UnityUIAgentEditor.Create());
            #else
            _agent = InitializeAgent(UnityUIAgentRuntime.Create(GetRoot()));
            #endif

            CurrentInterfaceType = _agent.GetInterfaceType();
        }

        /*
         * Public.
         */

        internal static bool IsTablet(IUnityUIAgent agent)
        {
            var diagonal = GetScreenDiagonalInches(agent);
            var aspect = GetScreenAspectRatio(agent);
            if (aspect >= 1.77)
                return diagonal >= 7F;
            return diagonal >= 5F;
        }

        internal static float GetScreenAspectRatio(IUnityUIAgent agent)
        {
            return Mathf.Max((float)agent.ScreenWidth, agent.ScreenHeight) / Mathf.Min(agent.ScreenWidth, agent.ScreenHeight);
        }

        internal static float GetScreenDiagonalInches(IUnityUIAgent agent)
        {
            var screenWidth = agent.ScreenWidth / Screen.dpi;
            var screenHeight = agent.ScreenHeight / Screen.dpi;
            return Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));
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
         * Private.
         */

        private static GameObject GetRoot()
        {
            if (_root)
                return _root;

            _root = new GameObject("[UnityUI]");
            Object.DontDestroyOnLoad(_root);
            return _root;
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