#if UNITY_EDITOR

using System;
using System.Linq;
using Build1.UnityUI.Utils.EGUI.Extensions;
using Build1.UnityUI.Utils.EGUI.RenderModes;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityUI.Utils.EGUI
{
    public static partial class EGUI
    {
        public static Enum Enum(Enum value, EnumRenderMode mode = EnumRenderMode.DropDown)
        {
            return mode switch
            {
                EnumRenderMode.DropDown   => RenderEnumDropDown(value),
                EnumRenderMode.Checkboxes => RenderEnumCheckboxes(value),
                _                         => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
            };
        }
        
        public static void Enum(ref Enum value, EnumRenderMode mode = EnumRenderMode.DropDown)
        {
            value = mode switch
            {
                EnumRenderMode.DropDown   => RenderEnumDropDown(value),
                EnumRenderMode.Checkboxes => RenderEnumCheckboxes(value),
                _                         => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
            };
        }

        public static void Enum(Enum value, Action<Enum> onChange)
        {
            Enum(value, EnumRenderMode.DropDown, onChange);
        }
        
        public static void Enum(Enum value, EnumRenderMode mode, Action<Enum> onChange)
        {
            var valueNew = mode switch
            {
                EnumRenderMode.DropDown   => RenderEnumDropDown(value),
                EnumRenderMode.Checkboxes => RenderEnumCheckboxes(value),
                _                         => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
            };

            if (!Equals(valueNew, value))
                onChange.Invoke(valueNew);
        }
        
        public static void Enum(Enum value, int height, Action<Enum> onChange)
        {
            var heightTemp = EditorStyles.popup.fixedHeight;
            EditorStyles.popup.fixedHeight = height;

            var valueNew = RenderEnumDropDown(value);

            EditorStyles.popup.fixedHeight = heightTemp;

            if (!Equals(valueNew, value))
                onChange.Invoke(valueNew);
        }
        
        /*
         * Rendering.
         */

        private static Enum RenderEnumDropDown(Enum value)
        {
            return value.IsFlags() ? RenderFlagsDropDown(value) : EditorGUILayout.EnumPopup(value);
        }

        private static Enum RenderEnumCheckboxes(Enum value)
        {
            var valueNew = value;
            var isFlags = value.IsFlags();
            
            Horizontally(() =>
            {
                void OnChange(Enum valueIn, bool selected)
                {
                    if (isFlags)
                    {
                        var valueInt = (int)Convert.ChangeType(value, TypeCode.Int32);
                        var valueDelta = (int)Convert.ChangeType(valueIn, TypeCode.Int32);
                        
                        int valueNewInt; 
                        if (selected)
                            valueNewInt = valueInt | valueDelta;
                        else
                            valueNewInt = valueInt & ~valueDelta;
                        
                        valueNew = (Enum)System.Enum.ToObject(value.GetType(), valueNewInt);
                    }
                    else
                    {
                        valueNew = valueIn;    
                    }
                }

                var values = System.Enum.GetValues(value.GetType());
                var valuesCasted = values.Cast<Enum>();
                foreach (var flag in valuesCasted)
                    Checkbox(flag.ToString(), value.HasFlag(flag), valueNewImpl => OnChange(flag, valueNewImpl));

                if (isFlags)
                {
                    if (Button("All", 45))
                        valueNew = (Enum)System.Enum.ToObject(value.GetType(), values.Cast<int>().Sum());

                    if (Button("None", 45))
                        valueNew = (Enum)System.Enum.ToObject(value.GetType(), 0);    
                }
            });
            
            return valueNew;
        }

        private static Enum RenderFlagsDropDown(Enum value)
        {
            var listFull = System.Enum.GetValues(value.GetType()).Cast<Enum>();
            var list = listFull.Where(value.HasFlag).ToList();
            var label = list.Count > 0 ? string.Join(" ", list) : "Nothing";

            var valueNewImpl = EditorGUILayout.EnumFlagsField(value);

            var positionLast = GUILayoutUtility.GetLastRect();
            var positionRect = new Rect(positionLast.x + 2, positionLast.y + 2, positionLast.width - 20, positionLast.height - 4);
            
            if (EditorGUIUtility.isProSkin)
                EditorGUI.DrawRect(positionRect, new Color(0.3176F, 0.3176F, 0.3176F));
            else
                EditorGUI.DrawRect(positionRect, new Color(0.8745F, 0.8745F, 0.8745F));

            var position = new Rect(positionLast.x + 3, positionLast.y - 1, positionRect.width, positionLast.height);
            EditorGUI.LabelField(position, label);

            return valueNewImpl;
        }
    }
}

#endif