#if UNITY_EDITOR

using Build1.UnityEGUI;
using Build1.UnityEGUI.Editors;
using Build1.UnityEGUI.RenderModes;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityUI.Adaptive.Editor
{
    [CustomEditor(typeof(AdaptiveAspectRatioFitterEnabler))]
    public sealed class AdaptiveAspectRatioFitterEnablerEditor : EGUIEditor
    {
        protected override void OnEGUI()
        {
            serializedObject.Update();

            var targetObject = (AdaptiveAspectRatioFitterEnabler)serializedObject.targetObject;
            var propertiesChanged = false;

            EGUI.Horizontally(() =>
            {
                EGUI.Label("Enabled on Interfaces", EGUI.Width(200));
                EGUI.Enum(targetObject.interfaceType, EnumRenderMode.DropDown, valueNew =>
                {
                    targetObject.interfaceType = (InterfaceType)valueNew;
                    propertiesChanged = true;
                });
            });

            EGUI.Horizontally(() =>
            {
                EGUI.Label("Stretch", EGUI.Width(200));
                EGUI.Checkbox(targetObject.stretch, valueNew =>
                {
                    targetObject.stretch = valueNew;
                    propertiesChanged = true;
                });    
            });
            
            EGUI.Horizontally(() =>
            {
                EGUI.Label("Reset offsets when ARF disabled", EGUI.Width(200));
                EGUI.Checkbox(targetObject.resetOffsetsWhenAspectRationFitterDisabled, valueNew =>
                {
                    targetObject.resetOffsetsWhenAspectRationFitterDisabled = valueNew;
                    propertiesChanged = true;
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