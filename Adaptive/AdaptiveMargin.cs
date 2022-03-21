using System.Linq;
using UnityEngine;

namespace Build1.UnityUI.Adaptive
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public sealed class AdaptiveMargin : MonoBehaviour
    {
        [SerializeField] public AdaptiveMarginItem[] items;
        
        private void Awake()
        {
            UnityUI.OnInterfaceTypeChanged += UpdateMargin;
        }

        private void OnEnable()
        {
            UpdateMargin(UnityUI.CurrentInterfaceType);
        }

        private void OnDestroy()
        {
            UnityUI.OnInterfaceTypeChanged -= UpdateMargin;
        }

        #if UNITY_EDITOR

        private void Reset()
        {
            if (items == null)
                items = new AdaptiveMarginItem[] { };    
            else
                UnityEditor.ArrayUtility.Clear(ref items);

            UnityEditor.ArrayUtility.Add(ref items, AdaptiveMarginItem.New(GetComponent<RectTransform>()));
        }

        #endif

        private void UpdateMargin(InterfaceType interfaceType)
        {
            foreach (var item in items)
            {
                if (item.rectTransform == null)
                    continue;

                var subItem = item.items.FirstOrDefault(i => (i.interfaceType & interfaceType) == interfaceType);
                if (subItem == null)
                    continue;
                
                item.rectTransform.offsetMin = new Vector2(subItem.padding.left, subItem.padding.bottom);
                item.rectTransform.offsetMax = new Vector2(-subItem.padding.right, -subItem.padding.top);
            }
        }
    }
}