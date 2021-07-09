#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Build1.UnityUI.TextMeshPro
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(TextMeshProContentSizeFitter), true)]
    public sealed class TextMeshProContentSizeFitterEditor : Editor
    {
        private SerializedProperty textMeshProProperty;
        
        private SerializedProperty horizontalFitProperty;
        private SerializedProperty widthMinProperty;
        private SerializedProperty widthMaxProperty;
        
        private SerializedProperty verticalFitProperty;
        private SerializedProperty heightMinProperty;
        private SerializedProperty heightMaxProperty;

        public void OnEnable()
        {
            textMeshProProperty = serializedObject.FindProperty("_textMeshProUGUI");
            
            horizontalFitProperty = serializedObject.FindProperty("_horizontalFit");
            widthMinProperty = serializedObject.FindProperty("_widthMin");
            widthMaxProperty = serializedObject.FindProperty("_widthMax");
            
            verticalFitProperty = serializedObject.FindProperty("_verticalFit");
            heightMinProperty = serializedObject.FindProperty("_heightMin");
            heightMaxProperty = serializedObject.FindProperty("_heightMax");
        }

        public override void OnInspectorGUI()
        {
            var fitter = (TextMeshProContentSizeFitter)target;
            
            serializedObject.Update();

            GUI.enabled = false;
            EditorGUILayout.PropertyField(textMeshProProperty, true);
            EditorGUILayout.Space(7);
            GUI.enabled = true;
            
            EditorGUILayout.PropertyField(horizontalFitProperty, true);

            if (fitter.HorizontalFit != TextMeshProContentSizeFitter.FitMode.Unconstrained)
            {
                EditorGUILayout.PropertyField(widthMinProperty, true);
                EditorGUILayout.PropertyField(widthMaxProperty, true);
                EditorGUILayout.Space(7);
            }

            EditorGUILayout.PropertyField(verticalFitProperty, true);

            if (fitter.VerticalFit != TextMeshProContentSizeFitter.FitMode.Unconstrained)
            {
                EditorGUILayout.PropertyField(heightMinProperty, true);
                EditorGUILayout.PropertyField(heightMaxProperty, true);    
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif