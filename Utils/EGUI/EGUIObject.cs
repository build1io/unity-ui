#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Build1.UnityUI.Utils.EGUI
{
    public static partial class EGUI
    {
        public static void Object<T>(T @object, bool isPersistent, Action<T> onChanged) where T : Object
        {
            var objectNew = EditorGUILayout.ObjectField(@object, typeof(T), true) as T;
            if (objectNew != @object && EditorUtility.IsPersistent(objectNew) == isPersistent)
                onChanged.Invoke(objectNew);
        }
        
        public static void Object<T>(T @object, bool isPersistent, int width, Action<T> onChanged) where T : Object
        {
            var objectNew = EditorGUILayout.ObjectField(@object, typeof(T), true, GUILayout.Width(width)) as T;
            if (objectNew != @object && EditorUtility.IsPersistent(objectNew) == isPersistent)
                onChanged.Invoke(objectNew);
        }
    }
}

#endif