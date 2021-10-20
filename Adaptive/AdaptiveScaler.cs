using System.Linq;
using Build1.UnityUI.Utils;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityUI.Adaptive
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public sealed class AdaptiveScaler : MonoBehaviour
    {
        [SerializeField] public AdaptiveScalerItem[] items;
        
        #if UNITY_EDITOR
        
        private bool CanUpdate => gameObject != null && gameObject.activeInHierarchy && items != null && items.Length > 0;
        
        #else
        
        private bool CanUpdate => items != null && items.Length > 0;
        
        #endif
        
        private void Awake()
        {
            if (Application.isPlaying)
                ScreenUtil.SubscribeOnScreenResolutionChanged(UpdateScale);

            UpdateScale();
        }

        private void OnDestroy()
        {
            if (Application.isPlaying)
                ScreenUtil.UnsubscribeFromScreenResolutionChanged(UpdateScale);
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
            UpdateScaleImpl(interfaceType);
        }
        
        private void Reset()
        {
            if (items == null)
                items = new AdaptiveScalerItem[] { };    
            else
                ArrayUtility.Clear(ref items);

            ArrayUtility.Add(ref items, AdaptiveScalerItem.New(gameObject));
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
            UpdateScale();
        }

        #endif

        private void UpdateScale()
        {
            if (!CanUpdate)
                return;

            var interfaceType = InterfaceUtil.GetInterfaceType(Application.platform, SystemInfo.deviceType);
            UpdateScaleImpl(interfaceType);
        }

        private void UpdateScaleImpl(InterfaceType interfaceType)
        {
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