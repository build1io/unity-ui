#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityUI.Utils.EGUI
{
    public static partial class EGUI
    {
        /*
         * Space.
         */

        public static void Space()
        {
            GUILayout.FlexibleSpace();
        }
        
        public static void Space(int pixels)
        {
            EditorGUILayout.Space(pixels);
        }
        
        /*
         * Layout.
         */
        
        public static void Horizontally(Action onHorizontally)
        {
            EditorGUILayout.BeginHorizontal();
            onHorizontally?.Invoke();
            EditorGUILayout.EndHorizontal();
        }
        
        public static void Vertically(Action onHorizontally)
        {
            EditorGUILayout.BeginVertical();
            onHorizontally?.Invoke();
            EditorGUILayout.EndVertical();
        }
        
        /*
         * Panel.
         */

        public static void Panel(int padding, Action onPanel)
        {
            var style = new GUIStyle(EditorStyles.helpBox)
            {
                padding = new RectOffset(padding, padding, padding, padding)
            };
            EditorGUILayout.BeginVertical(style);
            onPanel?.Invoke();
            EditorGUILayout.EndVertical();
        }

        /*
         * Texts.
         */

        public static void Label(string text)
        {
            GUILayout.Label(text);
        }
        
        public static void Label(string text, int width)
        {
            GUILayout.Label(text, GUILayout.Width(width));
        }
        
        public static void Label(string text, FontStyle fontStyle)
        {
            var style = new GUIStyle(GUI.skin.label)
            {
                fontStyle = fontStyle
            };
            GUILayout.Label(text, style);
        }
        
        /*
         * Numeric Fields.
         */
        
        public static void IntField(int value, int width, Action<int> onChanged)
        {
            var valueNew = EditorGUILayout.IntField(value, GUILayout.Width(width));
            if (valueNew != value)
                onChanged?.Invoke(valueNew);
        }
        
        public static void FloatField(float value, Action<float> onChanged)
        {
            var valueNew = EditorGUILayout.FloatField(value);
            if (valueNew != value)
                onChanged?.Invoke(valueNew);
        }
        
        public static void FloatField(float value, int width, Action<float> onChanged)
        {
            var valueNew = EditorGUILayout.FloatField(value, GUILayout.Width(width));
            if (valueNew != value)
                onChanged?.Invoke(valueNew);
        }
        
        /*
         * Message Box.
         */

        public static void MessageBox(string message, MessageType messageType)
        {
            EditorGUILayout.HelpBox(message, messageType);
        }
    }
}

#endif