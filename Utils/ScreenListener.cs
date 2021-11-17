using System;
using UnityEngine;

namespace Build1.UnityUI.Utils
{
    [DisallowMultipleComponent]
    public sealed class ScreenListener : MonoBehaviour
    {
        public event Action OnResolutionChanged;
        public event Action OnOrientationChanged;

        private ScreenOrientation _orientation;
        private int               _x;
        private int               _y;
        
        private void Awake()
        {
            _x = Screen.width;
            _y = Screen.height;
        }

        private void Update()
        {
            if (_orientation != Screen.orientation)
            {
                _orientation = Screen.orientation;
                
                OnOrientationChanged?.Invoke();
            }
            
            if (_x != Screen.width || _y != Screen.height)
            {
                _x = Screen.width;
                _y = Screen.height;

                OnResolutionChanged?.Invoke();    
            }
        }
    }
}