#if UNITY_EDITOR

using System;
using UnityEngine;

namespace Build1.UnityUI.Utils.EGUI
{
    public static partial class EGUI
    {
        public static bool Checkbox(string label, bool value)
        {
            return GUILayout.Toggle(value, $" {label}");
        }
        
        public static void Checkbox(string label, ref bool value)
        {
            value = GUILayout.Toggle(value, $" {label}");
        }

        public static void Checkbox(string label, bool value, Action<bool> onChanged)
        {
            var newValue = GUILayout.Toggle(value, $" {label}");
            if (newValue != value)
                onChanged?.Invoke(newValue);
        }
    }
}

#endif