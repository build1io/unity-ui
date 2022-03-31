using System.Linq;
using UnityEngine;

namespace Build1.UnityUI.Adaptive
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public sealed class AdaptiveScaler : MonoBehaviour
    {
        [SerializeField] public AdaptiveScalerItem[] items;

        private void OnEnable()
        {
            UnityUI.OnInterfaceTypeChanged += UpdateScale;
            
            UpdateScale(UnityUI.CurrentInterfaceType);
        }

        private void OnDisable()
        {
            UnityUI.OnInterfaceTypeChanged -= UpdateScale;
        }

        #if UNITY_EDITOR

        private void Reset()
        {
            if (items == null)
                items = new AdaptiveScalerItem[] { };    
            else
                UnityEditor.ArrayUtility.Clear(ref items);

            UnityEditor.ArrayUtility.Add(ref items, AdaptiveScalerItem.New(gameObject));
        }
        
        private void OnValidate()
        {
            UnityEditor.EditorApplication.delayCall += () =>
            {
                UpdateScale(UnityUI.CurrentInterfaceType);
            };
        }

        #endif

        private void UpdateScale(InterfaceType interfaceType)
        {
            if (items == null)
                return;
            
            foreach (var item in items)
            {
                if (item.gameObject == null)
                    continue;

                var subItem = item.items.FirstOrDefault(i => (i.interfaceType & interfaceType) == interfaceType);
                if (subItem != null)
                    item.gameObject.transform.localScale = new Vector3(subItem.scale, subItem.scale, subItem.scale);
            }
        }
    }
}