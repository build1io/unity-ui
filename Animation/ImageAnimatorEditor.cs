#if UNITY_EDITOR

using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityUI.Animation
{
    [CustomEditor(typeof(ImageAnimator))]
    public sealed class ImageAnimatorEditor : Editor
    {
        private bool _eventsUnfolded; 
        
        private SerializedProperty framesPerSecondProperty;
        private SerializedProperty startFrameProperty;
        private SerializedProperty previewFrameProperty;
        private SerializedProperty loopProperty;
        private SerializedProperty playOnAwakeProperty;
        private SerializedProperty spritesProperty;
        private SerializedProperty onFrameChangedProperty;
        private SerializedProperty onCompleteProperty;
        
        public void OnEnable()
        {
            framesPerSecondProperty = serializedObject.FindProperty("framesPerSecond");
            startFrameProperty = serializedObject.FindProperty("startFrame");
            previewFrameProperty = serializedObject.FindProperty("previewFrame");
            loopProperty = serializedObject.FindProperty("loop");
            playOnAwakeProperty = serializedObject.FindProperty("playOnAwake");
            spritesProperty = serializedObject.FindProperty("sprites");
            onFrameChangedProperty = serializedObject.FindProperty("onFrameChanged");
            onCompleteProperty = serializedObject.FindProperty("onComplete");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUILayout.PropertyField(framesPerSecondProperty);
            EditorGUILayout.PropertyField(startFrameProperty);
            EditorGUILayout.PropertyField(previewFrameProperty);
            EditorGUILayout.PropertyField(loopProperty);
            EditorGUILayout.PropertyField(playOnAwakeProperty);
            EditorGUILayout.Space(7);
            
            DropAreaGUI();
            
            EditorGUILayout.Space(3);
            EditorGUILayout.PropertyField(spritesProperty);
            EditorGUILayout.Space(2);
            
            var style = new GUIStyle(EditorStyles.foldout)
            {
                fontStyle = FontStyle.Bold
            };

            _eventsUnfolded = EditorGUILayout.Foldout(_eventsUnfolded, "Events", true, style);
            if (_eventsUnfolded)
            {
                EditorGUILayout.Space(4);
                EditorGUILayout.PropertyField(onFrameChangedProperty);
                EditorGUILayout.PropertyField(onCompleteProperty);    
            }
            else
            {
                EditorGUILayout.Space(2);
            }

            serializedObject.ApplyModifiedProperties();
        }

        public void DropAreaGUI()
        {
            var @event = Event.current;
            var area = GUILayoutUtility.GetRect(0.0f, 50.0f, GUILayout.ExpandWidth(true));

            var style = new GUIStyle(GUI.skin.box)
            {
                alignment = TextAnchor.MiddleCenter
            };
            
            GUI.Box(area, "Drop sprite here", style);
            
            switch (@event.type)
            {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                    if (!area.Contains(@event.mousePosition))
                        return;

                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                    if (@event.type == EventType.DragPerform)
                    {
                        DragAndDrop.AcceptDrag();

                        var result = EditorUtility.DisplayDialogComplex("Image Animator", "Add or Replace sprites?", "Add", "Cancel", "Replace");
                        if (result == 1)
                            break;
                        
                        if (result == 2)
                            spritesProperty.ClearArray();
                        
                        foreach (var draggedObject in DragAndDrop.objectReferences)
                        {
                            if (!(draggedObject is Texture2D)) 
                                continue;
                            
                            var path = AssetDatabase.GetAssetPath(draggedObject.GetInstanceID());
                            var objects = AssetDatabase.LoadAllAssetsAtPath(path);
                            var sprites = objects.Where(q => q is Sprite).Cast<Sprite>().ToArray();

                            var index = spritesProperty.arraySize;
                            foreach (var sprite in sprites)
                            {
                                spritesProperty.InsertArrayElementAtIndex(index);
                                spritesProperty.GetArrayElementAtIndex(index).objectReferenceValue = sprite;
                                index++;
                            }
                            
                            break;
                        }
                    }
                    break;
            }
        }
    }
}

#endif