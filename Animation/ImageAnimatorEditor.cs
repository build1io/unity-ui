#if UNITY_EDITOR

using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityUI.Animation
{
    [CustomEditor(typeof(ImageAnimator))]
    public sealed class ImageAnimatorEditor : Editor
    {
        private SerializedProperty framesPerSecondProperty;
        private SerializedProperty startFrameProperty;
        private SerializedProperty loopProperty;
        private SerializedProperty playOnAwakeProperty;
        private SerializedProperty spritesProperty;
        
        public void OnEnable()
        {
            framesPerSecondProperty = serializedObject.FindProperty("framesPerSecond");
            startFrameProperty = serializedObject.FindProperty("startFrame");
            loopProperty = serializedObject.FindProperty("loop");
            playOnAwakeProperty = serializedObject.FindProperty("playOnAwake");
            spritesProperty = serializedObject.FindProperty("sprites");
        }

        public override void OnInspectorGUI()
        {
            var imageAnimator = (ImageAnimator)target;

            serializedObject.Update();
            
            EditorGUILayout.PropertyField(framesPerSecondProperty);
            EditorGUILayout.PropertyField(startFrameProperty);
            EditorGUILayout.PropertyField(loopProperty);
            EditorGUILayout.PropertyField(playOnAwakeProperty);
            EditorGUILayout.Space(7);
            EditorGUILayout.PropertyField(spritesProperty);
            EditorGUILayout.Space(5);
            
            DropAreaGUI(imageAnimator);
            
            EditorGUILayout.Space(5);
            
            serializedObject.ApplyModifiedProperties();
        }

        public void DropAreaGUI(ImageAnimator imageAnimator)
        {
            var @event = Event.current;
            var area = GUILayoutUtility.GetRect(0.0f, 50.0f, GUILayout.ExpandWidth(true));

            var style = new GUIStyle(GUI.skin.box)
            {
                alignment = TextAnchor.MiddleCenter
            };
            
            GUI.Box(area, "Drop main sprite here", style);
            
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
                        foreach (var draggedObject in DragAndDrop.objectReferences)
                        {
                            if (!(draggedObject is Texture2D)) 
                                continue;
                            
                            var path = AssetDatabase.GetAssetPath(draggedObject.GetInstanceID());
                            var objects = AssetDatabase.LoadAllAssetsAtPath(path);
                            
                            imageAnimator.Sprites = objects.Where(q => q is Sprite).Cast<Sprite>().ToArray();
                            break;
                        }
                    }
                    break;
            }
        }
    }
}

#endif