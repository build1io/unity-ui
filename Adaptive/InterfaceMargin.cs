using System.Linq;
using Build1.UnityUI.Utils;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityUI.Adaptive
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public sealed class InterfaceMargin : MonoBehaviour
    {
        [SerializeField] public InterfaceMarginItem[] items;
        
        private bool CanUpdate => items != null && items.Length > 0;

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
                items = new InterfaceMarginItem[] { };    
            else
                ArrayUtility.Clear(ref items);

            ArrayUtility.Add(ref items, InterfaceMarginItem.New(GetComponent<RectTransform>()));
        }

        private void OnValidate()
        {
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