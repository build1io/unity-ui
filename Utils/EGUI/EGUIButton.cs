#if UNITY_EDITOR

using System;
using UnityEngine;

namespace Build1.UnityUI.Utils.EGUI
{
    public static partial class EGUI
    {
        public static float ButtonHeight01 { get; set; } = 30;
        public static float ButtonHeight02 { get; set; } = 25;
        public static float ButtonHeight03 { get; set; } = 22;

        /*
         * Return.
         */

        public static bool Button(string label)                                              { return GUILayout.Button(label); }
        public static bool Button(string label, int width)                                   { return GUILayout.Button(label, GUILayout.Width(width)); }
        public static bool Button(string label, float height)                                { return GUILayout.Button(label, GUILayout.Height(height)); }
        public static bool Button(string label, int width, float height)                     { return GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height)); }

        public static bool Button(string label, int width, float height, RectOffset padding)
        {
            var style = GUI.skin.button;
            style.padding = padding;
            return GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Height(height));
        }

        /*
         * Out.
         */

        public static void Button(string label, out bool clicked)                          { clicked = GUILayout.Button(label); }
        public static void Button(string label, out bool clicked, int width)               { clicked = GUILayout.Button(label, GUILayout.Width(width)); }
        public static void Button(string label, out bool clicked, float height)            { clicked = GUILayout.Button(label, GUILayout.Height(height)); }
        public static void Button(string label, out bool clicked, int width, float height) { clicked = GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height)); }

        /*
         * Callback.
         */

        public static void Button(string label, Action onClicked)
        {
            var clicked = GUILayout.Button(label);
            if (clicked)
                onClicked.Invoke();
        }

        public static void Button(string label, int width, Action onClicked)
        {
            var clicked = GUILayout.Button(label, GUILayout.Width(width));
            if (clicked)
                onClicked.Invoke();
        }

        public static void Button(string label, int width, float height, TextAnchor alignment, Action onClicked)
        {
            var style = new GUIStyle(GUI.skin.button)
            {
                alignment = alignment
            };
            var clicked = GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Height(height));
            if (clicked)
                onClicked.Invoke();
        }

        public static void Button(string label, int width, float height, Action onClicked)
        {
            var clicked = GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height));
            if (clicked)
                onClicked.Invoke();
        }

        public static void Button(string label, int width, float height, RectOffset padding, Action onClicked)
        {
            var style = GUI.skin.button;
            style.padding = padding;

            var clicked = GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Height(height));
            if (clicked)
                onClicked.Invoke();
        }

        /*
         * Callback with Parameter.
         */

        public static void Button<T>(string label, Action<T> onClicked, T param)
        {
            var clicked = GUILayout.Button(label);
            if (clicked)
                onClicked.Invoke(param);
        }

        public static void Button<T>(string label, int width, Action<T> onClicked, T param)
        {
            var clicked = GUILayout.Button(label, GUILayout.Width(width));
            if (clicked)
                onClicked.Invoke(param);
        }

        public static void Button<T>(string label, float height, Action<T> onClicked, T param)
        {
            var clicked = GUILayout.Button(label, GUILayout.Height(height));
            if (clicked)
                onClicked.Invoke(param);
        }

        public static void Button<T>(string label, int width, float height, Action<T> onClicked, T param)
        {
            var clicked = GUILayout.Button(label, GUILayout.Width(width), GUILayout.Height(height));
            if (clicked)
                onClicked.Invoke(param);
        }

        public static void Button<T>(string label, int width, float height, RectOffset padding, Action<T> onClicked, T param)
        {
            var style = GUI.skin.button;
            style.padding = padding;

            var clicked = GUILayout.Button(label, style, GUILayout.Width(width), GUILayout.Height(height));
            if (clicked)
                onClicked.Invoke(param);
        }
    }
}

#endif