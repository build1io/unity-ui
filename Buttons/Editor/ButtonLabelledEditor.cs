#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.UI;

namespace Build1.UnityUI.Buttons.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ButtonLabelled), true)]
    public sealed class ButtonLabelledEditor : ButtonEditor
    {
        private SerializedProperty labelProperty;
        private SerializedProperty colorNormalProperty;
        private SerializedProperty colorHighlightedProperty;
        private SerializedProperty colorPressedProperty;
        private SerializedProperty colorSelectedProperty;
        private SerializedProperty colorDisabledProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            labelProperty = serializedObject.FindProperty("_label");
            colorNormalProperty = serializedObject.FindProperty("_colorNormal");
            colorHighlightedProperty = serializedObject.FindProperty("_colorHighlighted");
            colorPressedProperty = serializedObject.FindProperty("_colorPressed");
            colorSelectedProperty = serializedObject.FindProperty("_colorSelected");
            colorDisabledProperty = serializedObject.FindProperty("_colorDisabled");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            serializedObject.Update();
            
            EditorGUILayout.LabelField("Label", EditorStyles.boldLabel); 
            EditorGUILayout.PropertyField(labelProperty);
            EditorGUILayout.PropertyField(colorNormalProperty);
            EditorGUILayout.PropertyField(colorHighlightedProperty);
            EditorGUILayout.PropertyField(colorPressedProperty);
            EditorGUILayout.PropertyField(colorSelectedProperty);
            EditorGUILayout.PropertyField(colorDisabledProperty);
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif