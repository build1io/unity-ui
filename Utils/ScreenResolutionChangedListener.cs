using System;
using UnityEngine;

namespace Build1.UnityUI.Utils
{
    [DisallowMultipleComponent]
    public sealed class ScreenResolutionChangedListener : MonoBehaviour
    {
        public event Action OnChanged;

        private int _x;
        private int _y;

        private void Awake()
        {
            _x = Screen.width;
            _y = Screen.height;
        }

        private void Update()
        {
            if (_x == Screen.width && _y == Screen.height) 
                return;

            _x = Screen.width;
            _y = Screen.height;

            OnChanged?.Invoke();
        }
    }
}