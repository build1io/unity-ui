#if UNITY_EDITOR

using System.Linq;
using Build1.UnityUI.Utils.EGUI;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityUI.Adaptive.Editor
{
    [CustomEditor(typeof(AdaptiveScaler))]
    public sealed class AdaptiveScalerEditor : UnityEditor.Editor
    {
        private SerializedProperty items;

        public void OnEnable()
        {
            items = serializedObject.FindProperty("items");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var targetObject = (AdaptiveScaler)serializedObject.targetObject;
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
                    EGUI.Label("Game Object", 200);
                    EGUI.Label("Scales");
                });
                EGUI.Space(2);
                
                for (var i = 0; i < items.arraySize; i++)
                {
                    var item = targetObject.items.ElementAtOrDefault(i);
                    if (item == null)
                        continue;
                         
                    EGUI.Horizontally(() =>
                    {
                        EGUI.Object(item.gameObject, false, 200, gameObjectNew =>
                        {
                            item.gameObject = gameObjectNew;
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
                                    EGUI.FloatField(subItem.scale, scaleNew =>
                                    {
                                        subItem.scale = scaleNew;
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
                        ArrayUtility.Add(ref targetObject.items, AdaptiveScalerItem.New(null));
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