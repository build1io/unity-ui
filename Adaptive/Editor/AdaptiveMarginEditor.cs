#if UNITY_EDITOR

using Build1.UnityEGUI;
using Build1.UnityEGUI.Editors;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityUI.Adaptive.Editor
{
    [CustomEditor(typeof(AdaptiveMargin))]
    public sealed class AdaptiveMarginEditor : EGUIEditor
    {
        private SerializedProperty items;

        public void OnEnable()
        {
            items = serializedObject.FindProperty("items");
        }

        protected override void OnEGUI()
        {
            serializedObject.Update();

            var targetObject = (AdaptiveMargin)serializedObject.targetObject;
            var propertiesChanged = false;

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

                                    EGUI.Int(subItem.padding.left, EGUI.Width(60))
                                        .OnChange(left =>
                                         {
                                             subItem.padding.left = left;
                                             propertiesChanged = true;
                                         });

                                    EGUI.Int(subItem.padding.right, EGUI.Width(60))
                                        .OnChange(right =>
                                         {
                                             subItem.padding.right = right;
                                             propertiesChanged = true;
                                         });

                                    EGUI.Int(subItem.padding.top, EGUI.Width(60))
                                        .OnChange(top =>
                                         {
                                             subItem.padding.top = top;
                                             propertiesChanged = true;
                                         });

                                    EGUI.Int(subItem.padding.bottom, EGUI.Width(60))
                                        .OnChange(bottom =>
                                         {
                                             subItem.padding.bottom = bottom;
                                             propertiesChanged = true;
                                         });
                                });
                            }
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