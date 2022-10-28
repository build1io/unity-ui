#if UNITY_EDITOR

using Build1.UnityEGUI;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityUI.Adaptive.Editor
{
    [CustomEditor(typeof(AdaptiveMargin))]
    public sealed class AdaptiveMarginEditor : UnityEditor.Editor
    {
        private SerializedProperty items;

        public void OnEnable()
        {
            items = serializedObject.FindProperty("items");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var targetObject = (AdaptiveMargin)serializedObject.targetObject;
            var propertiesChanged = false;

            EGUI.Horizontally(() =>
            {
                EGUI.Label("Managed Objects", EGUI.FontStyle(FontStyle.Bold));
                EGUI.Space();
                EGUI.Label("Count:", EGUI.Width(40));
                EGUI.Int(items.arraySize, 50, value => { items.arraySize = value; });
            });

            EGUI.Panel(10, () =>
            {
                EGUI.Horizontally(() =>
                {
                    EGUI.Label("Rect Transform");
                    EGUI.Label(" Interface", EGUI.Width(75));
                    EGUI.Label("Left", EGUI.Width(60));
                    EGUI.Label("Right", EGUI.Width(60));
                    EGUI.Label("Top", EGUI.Width(60));
                    EGUI.Label("Bottom", EGUI.Width(60));
                    EGUI.Label(string.Empty, EGUI.Width(30));
                });
                EGUI.Space(2);
                
                for (var i = 0; i < items.arraySize; i++)
                {
                    var item = targetObject.items[i];
                         
                    EGUI.Horizontally(() =>
                    {
                        EGUI.Object(item.rectTransform, false, rectTransformNew =>
                        {
                            item.rectTransform = rectTransformNew;
                            propertiesChanged = true;
                        });
                        
                        EGUI.Vertically(() =>
                        {
                            for (var j = 0; j < item.items.Length; j++)
                            {
                                var subItem = item.items[j];
                                
                                EGUI.Horizontally(() =>
                                {
                                    EGUI.Label(subItem.interfaceType.ToString(), EGUI.Width(75));
                                    
                                    EGUI.Int(subItem.padding.left, 60, left =>
                                    {
                                        subItem.padding.left = left;
                                        propertiesChanged = true;
                                    });
                                    
                                    EGUI.Int(subItem.padding.right, 60, right =>
                                    {
                                        subItem.padding.right = right;
                                        propertiesChanged = true;
                                    });
                                    
                                    EGUI.Int(subItem.padding.top, 60, top =>
                                    {
                                        subItem.padding.top = top;
                                        propertiesChanged = true;
                                    });
                                    
                                    EGUI.Int(subItem.padding.bottom, 60, bottom =>
                                    {
                                        subItem.padding.bottom = bottom;
                                        propertiesChanged = true;
                                    });
                                });
                            }
                        });
            
                        if (EGUI.Button("-", 30, 18, new RectOffset(1, 1, 0, 2)))
                            ArrayUtility.Remove(ref targetObject.items, item);
                    });
                    EGUI.Space(2);
                }
            
                EGUI.Horizontally(() =>
                {
                    EGUI.Space();
                    if (EGUI.Button("+", 30, 25, new RectOffset(1, 1, 0, 2)))
                        ArrayUtility.Add(ref targetObject.items, AdaptiveMarginItem.New(null));
                });
            
                
            });

            serializedObject.ApplyModifiedProperties();
            
            if (propertiesChanged)
            {
                targetObject.SendMessage("OnValidate", null, SendMessageOptions.DontRequireReceiver);
                EditorUtility.SetDirty(targetObject);
            }
        }
        
    }
}

#endif