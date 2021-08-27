#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Build1.UnityUI.Adaptive.Editor
{
    [CustomEditor(typeof(InterfaceActivator)), CanEditMultipleObjects]
    public sealed class InterfaceActivatorEditor : UnityEditor.Editor
    {
        private SerializedProperty items;
        
        public void OnEnable()
        {
            items = serializedObject.FindProperty("items");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUILayout.PropertyField(items, new GUIContent("Configuration:"), true);

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif