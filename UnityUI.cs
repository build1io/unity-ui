using System;
using Build1.UnityUI.Agents;
using UnityEngine;

namespace Build1.UnityUI
{
    public static class UnityUI
    {
        public static InterfaceType     CurrentInterfaceType { get; private set; }
        public static DeviceOrientation DeviceOrientation    => _agent.DeviceOrientation;
        public static ScreenOrientation ScreenOrientation    => _agent.ScreenOrientation;
        public static int               ScreenWidth          => _agent.ScreenWidth;
        public static int               ScreenHeight         => _agent.ScreenHeight;
        public static float             ScreenDPI            => Screen.dpi;

        public static event Action<InterfaceType> OnInterfaceTypeChanged;
        public static event Action                OnDeviceOrientationChanged;
        public static event Action                OnScreenOrientationChanged;
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
        
        public static void SetAvailableOrientations(ScreenOrientations orientations)
        {
            switch (orientations)
            {
                case ScreenOrientations.Portrait:
                    Screen.orientation = ScreenOrientation.Portrait;
                    return;
                case ScreenOrientations.PortraitUpsideDown:
                    Screen.orientation = ScreenOrientation.PortraitUpsideDown;
                    return;
                case ScreenOrientations.LandscapeLeft:
                    Screen.orientation = ScreenOrientation.LandscapeLeft;
                    return;
                case ScreenOrientations.LandscapeRight:
                    Screen.orientation = ScreenOrientation.LandscapeRight;
                    return;
            }

            var autorotateToPortrait = (orientations & ScreenOrientations.Portrait) == ScreenOrientations.Portrait;
            var autorotateToPortraitUpsideDown = (orientations & ScreenOrientations.PortraitUpsideDown) == ScreenOrientations.PortraitUpsideDown;
            var autorotateToLandscapeLeft = (orientations & ScreenOrientations.LandscapeLeft) == ScreenOrientations.LandscapeLeft;
            var autorotateToLandscapeRight = (orientations & ScreenOrientations.LandscapeRight) == ScreenOrientations.LandscapeRight;

            if (Screen.orientation != ScreenOrientation.AutoRotation)
            {
                if (autorotateToPortrait)
                    Screen.orientation = ScreenOrientation.Portrait;
                else if (autorotateToLandscapeLeft)
                    Screen.orientation = ScreenOrientation.LandscapeLeft;
                else if (autorotateToLandscapeRight)
                    Screen.orientation = ScreenOrientation.LandscapeRight;
                else if (autorotateToPortraitUpsideDown)
                    Screen.orientation = ScreenOrientation.PortraitUpsideDown;
            }

            Screen.autorotateToPortrait = autorotateToPortrait;
            Screen.autorotateToPortraitUpsideDown = autorotateToPortraitUpsideDown;
            Screen.autorotateToLandscapeLeft = autorotateToLandscapeLeft;
            Screen.autorotateToLandscapeRight = autorotateToLandscapeRight;
            Screen.orientation = ScreenOrientation.AutoRotation;
        }

        internal static bool IsTablet()
        {
            return GetScreenDiagonalInches() >= 7F && 
                   GetScreenAspectRatio() <= 1.77;
        }

        /*
         * Private.
         */

        private static IUnityUIAgent InitializeAgent(IUnityUIAgent agent)
        {
            agent.OnDeviceOrientationChanged += OnDeviceOrientationChangedHandler;
            agent.OnSomethingChanged += OnSomethingChangedHandler;
            return agent;
        }

        /*
         * Event Handlers.
         */

        private static void OnDeviceOrientationChangedHandler()
        {
            OnDeviceOrientationChanged?.Invoke();
        }
        
        private static void OnSomethingChangedHandler(bool screenOrientationChanged, bool screenResolutionChanged)
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

            if (screenOrientationChanged)
                OnScreenOrientationChanged?.Invoke();

            if (screenResolutionChanged)
                OnScreenResolutionChanged?.Invoke();

            if (interfaceTypeChanged)
                OnInterfaceTypeChanged?.Invoke(CurrentInterfaceType);

            if (screenOrientationChanged || screenResolutionChanged)
                OnSomethingChanged?.Invoke();
        }
    }
}