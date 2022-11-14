#if UNITY_EDITOR

using System.Linq;
using Build1.UnityEGUI;
using Build1.UnityEGUI.Editors;
using Build1.UnityEGUI.RenderModes;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityUI.Adaptive.Editor
{
    [CustomEditor(typeof(AdaptiveActivator)), CanEditMultipleObjects]
    public sealed class AdaptiveActivatorEditor : EGUIEditor
    {
        private SerializedProperty items;

        public void OnEnable()
        {
            items = serializedObject.FindProperty("items");
        }

        protected override void OnEGUI()
        {
            serializedObject.Update();

            var targetObject = (AdaptiveActivator)serializedObject.targetObject;
            var propertiesChanged = false;

            if (targetObject.items.FirstOrDefault(i => i.gameObject == targetObject.gameObject) != null)
            {
                EGUI.Space(3);
                EGUI.MessageBox("Making object controlling itself might result in unexpected behavior.", MessageType.Warning);
                EGUI.Space(5);    
            }

            EGUI.Horizontally(() =>
            {
                EGUI.Label("Managed Objects", EGUI.FontStyle(FontStyle.Bold));
                EGUI.Space();
                EGUI.Label("Count:", EGUI.Width(40));
                EGUI.Int(items.arraySize, EGUI.Width(50))
                    .OnChange(value => { items.arraySize = value; });
            });

            EGUI.Panel(10, () =>
            {
                EGUI.Horizontally(() =>
                {
                    EGUI.Label("Game Object", EGUI.Width(200));
                    EGUI.Label("Active on Interfaces");
                });
                EGUI.Space(2);
                
                for (var i = 0; i < items.arraySize; i++)
                {
                    var item = targetObject.items[i];
                    
                    EGUI.Horizontally(() =>
                    {
                        EGUI.Object(item.gameObject, false, 200, gameObjectNew =>
                        {
                            item.gameObject = gameObjectNew;
                            propertiesChanged = true;
                        });

                        EGUI.Enum(item.interfaceType, EnumRenderMode.DropDown, value =>
                        {
                            item.interfaceType = (InterfaceType)value;
                            propertiesChanged = true;
                        });

                        if (EGUI.Button("-", EGUI.Size(30, 18), EGUI.Padding(new RectOffset(1, 1, 0, 2))).Clicked())
                            ArrayUtility.Remove(ref targetObject.items, item);
                    });
                    EGUI.Space(2);
                }

                EGUI.Horizontally(() =>
                {
                    EGUI.Space();
                    if (EGUI.Button("+", EGUI.Size(30, 25), EGUI.Padding(new RectOffset(1, 1, 0, 2))).Clicked())
                        ArrayUtility.Add(ref targetObject.items, AdaptiveActivatorItem.New(null));
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