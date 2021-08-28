#if UNITY_EDITOR

using Build1.UnityUI.Utils.EGUI;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityUI.Adaptive.Editor
{
    [CustomEditor(typeof(InterfaceMargin))]
    public sealed class InterfaceMarginEditor : UnityEditor.Editor
    {
        private SerializedProperty items;

        public void OnEnable()
        {
            items = serializedObject.FindProperty("items");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var targetObject = (InterfaceMargin)serializedObject.targetObject;
            var propertiesChanged = false;

            EGUI.Horizontally(() =>
            {
                EGUI.Label("Managed Objects", FontStyle.Bold);
                EGUI.Space();
                EGUI.Label("Count:", 40);
                EGUI.IntField(items.arraySize, 50, value => { items.arraySize = value; });
            });

            EGUI.Panel(10, () =>
            {
                EGUI.Horizontally(() =>
                {
                    EGUI.Label("Game Object");
                    EGUI.Label("Interface", 75);
                    EGUI.Label("Left", 80);
                    EGUI.Label("Right", 80);
                    EGUI.Label("Top", 80);
                    EGUI.Label("Bottom", 80);
                    EGUI.Label(string.Empty, 30);
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
                                    EGUI.Label(subItem.interfaceType.ToString(), 75);
                                    
                                    EGUI.IntField(subItem.padding.left, 80, left =>
                                    {
                                        subItem.padding.left = left;
                                        propertiesChanged = true;
                                    });
                                    
                                    EGUI.IntField(subItem.padding.right, 80, right =>
                                    {
                                        subItem.padding.right = right;
                                        propertiesChanged = true;
                                    });
                                    
                                    EGUI.IntField(subItem.padding.top, 80, top =>
                                    {
                                        subItem.padding.top = top;
                                        propertiesChanged = true;
                                    });
                                    
                                    EGUI.IntField(subItem.padding.bottom, 80, bottom =>
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
                        ArrayUtility.Add(ref targetObject.items, InterfaceMarginItem.New(null));
                });
            
                if (propertiesChanged)
                    targetObject.SendMessage("OnValidate", null, SendMessageOptions.DontRequireReceiver);
            });

            serializedObject.ApplyModifiedProperties();
        }
        
    }
}

#endif