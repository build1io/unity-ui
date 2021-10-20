using Build1.UnityUI.Utils;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityUI.Adaptive
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public sealed class AdaptiveActivator : MonoBehaviour
    {
        [SerializeField] public AdaptiveActivatorItem[] items;

        #if UNITY_EDITOR
        
        private bool CanUpdate => gameObject != null && gameObject.activeInHierarchy && items != null && items.Length > 0;
        
        #else
        
        private bool CanUpdate => items != null && items.Length > 0;
        
        #endif

        private void Awake()
        {
            if (Application.isPlaying)
                ScreenUtil.SubscribeOnScreenResolutionChanged(UpdateActive);

            UpdateActive();
        }

        private void OnDestroy()
        {
            if (Application.isPlaying)
                ScreenUtil.UnsubscribeFromScreenResolutionChanged(UpdateActive);
        }

        #if UNITY_EDITOR

        private InterfaceType _interfaceType;

        private void Update()
        {
            if (!CanUpdate)
                return;

            var interfaceType = InterfaceUtil.GetInterfaceType(Application.platform, SystemInfo.deviceType);
            if (interfaceType == _interfaceType)
                return;

            _interfaceType = interfaceType;
            UpdateActiveImpl(interfaceType);
        }

        private void Reset()
        {
            if (items == null)
                items = new AdaptiveActivatorItem[] { };    
            else
                ArrayUtility.Clear(ref items);
            
            ArrayUtility.Add(ref items, AdaptiveActivatorItem.New(null));
        }

        private void OnValidate()
        {
            if (Application.isPlaying)
                return;
            
            EditorApplication.update += OnValidateImpl;
        }

        private void OnValidateImpl()
        {
            EditorApplication.update -= OnValidateImpl;
            UpdateActive();
        }

        #endif

        private void UpdateActive()
        {
            if (!CanUpdate)
                return;

            var interfaceType = InterfaceUtil.GetInterfaceType(Application.platform, SystemInfo.deviceType);
            UpdateActiveImpl(interfaceType);
        }

        private void UpdateActiveImpl(InterfaceType interfaceType)
        {
            foreach (var item in items)
            {
                if (item.gameObject != null)
                    item.gameObject.SetActive((item.interfaceType & interfaceType) == interfaceType);
            }
        }
    }
}