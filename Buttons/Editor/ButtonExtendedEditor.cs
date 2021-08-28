#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.UI;

namespace Build1.UnityUI.Buttons.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ButtonExtended), true)]
    public sealed class ButtonExtendedEditor : ButtonEditor
    {
        private SerializedProperty onUpProperty;
        private SerializedProperty onDownProperty;
        private SerializedProperty onOverProperty;
        private SerializedProperty onOutProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            onUpProperty = serializedObject.FindProperty("_onUp");
            onDownProperty = serializedObject.FindProperty("_onDown");
            onOverProperty = serializedObject.FindProperty("_onOver");
            onOutProperty = serializedObject.FindProperty("_onOut");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            serializedObject.Update();
            EditorGUILayout.PropertyField(onUpProperty, true);
            EditorGUILayout.PropertyField(onDownProperty, true);
            EditorGUILayout.PropertyField(onOverProperty, true);
            EditorGUILayout.PropertyField(onOutProperty, true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif