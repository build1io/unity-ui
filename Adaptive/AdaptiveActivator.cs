using UnityEngine;

namespace Build1.UnityUI.Adaptive
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public sealed class AdaptiveActivator : MonoBehaviour
    {
        [SerializeField] public AdaptiveActivatorItem[] items;

        private void Awake()
        {
            UnityUI.OnInterfaceTypeChanged += UpdateActive;
        }

        private void OnEnable()
        {
            UpdateActive(UnityUI.CurrentInterfaceType);
        }

        private void OnDestroy()
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