using System;
using UnityEngine;

namespace Build1.UnityUI.Utils
{
    public static class ScreenUtil
    {
        private static readonly ScreenListener _listener;

        private static ScreenOrientation ScreenOrientation => Screen.orientation;
        
        private static float ScreenWidth
        {
            get
            {
                #if UNITY_EDITOR
                    return Screen.currentResolution.width;
                #else
                    return Screen.width;
                #endif
            }
        }

        private static float ScreenHeight
        {
            get
            {
                #if UNITY_EDITOR
                    return Screen.currentResolution.height;
                #else
                    return Screen.height;
                #endif
            }
        }
        
        public static event Action OnOrientationChanged;
        public static event Action OnResolutionChanged; 

        static ScreenUtil()
        {
            var root = UnityUI.GetRoot();
            _listener = root.GetComponent<ScreenListener>();
            _listener ??= root.AddComponent<ScreenListener>();
            _listener.OnOrientationChanged += OnListenerOrientationChanged;
            _listener.OnResolutionChanged += OnListenerResolutionChanged;
        }

        /*
         * Screen Values.
         */

        public static float GetAspectRatio()
        {
            return Mathf.Max(ScreenWidth, ScreenHeight) / Mathf.Min(ScreenWidth, ScreenHeight);
        }

        public static float GetDiagonalInches()
        {
            var screenWidth = ScreenWidth / Screen.dpi;
            var screenHeight = ScreenHeight / Screen.dpi;
            return Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));
        }
        
        /*
         * Event Handlers.
         */

        private static void OnListenerOrientationChanged()
        {
            OnOrientationChanged?.Invoke();
        }
        
        private static void OnListenerResolutionChanged()
        {
            OnResolutionChanged?.Invoke();
        }
    }
}