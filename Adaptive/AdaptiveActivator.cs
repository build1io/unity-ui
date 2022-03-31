using UnityEngine;

namespace Build1.UnityUI.Adaptive
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public sealed class AdaptiveActivator : MonoBehaviour
    {
        [SerializeField] public AdaptiveActivatorItem[] items;

        private void OnEnable()
        {
            UnityUI.OnInterfaceTypeChanged += UpdateActive;
            
            UpdateActive(UnityUI.CurrentInterfaceType);
        }

        private void OnDisable()
        {
            UnityUI.OnInterfaceTypeChanged -= UpdateActive;
        }

        #if UNITY_EDITOR

        private void Reset()
        {
            if (items == null)
                items = new AdaptiveActivatorItem[] { };
            else
                UnityEditor.ArrayUtility.Clear(ref items);

            UnityEditor.ArrayUtility.Add(ref items, AdaptiveActivatorItem.New(null));
        }

        private void OnValidate()
        {
            UnityEditor.EditorApplication.delayCall += () =>
            {
                UpdateActive(UnityUI.CurrentInterfaceType);
            };
        }
        
        #endif

        private void UpdateActive(InterfaceType interfaceType)
        {
            foreach (var item in items)
            {
                if (item.gameObject != null)
                    item.gameObject.SetActive((item.interfaceType & interfaceType) == interfaceType);
            }
        }
    }
}