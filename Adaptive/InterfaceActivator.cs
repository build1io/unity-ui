using Build1.UnityUI.Utils;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityUI.Adaptive
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public sealed class InterfaceActivator : MonoBehaviour
    {
        [SerializeField] private InterfaceActivatorItem[] items;

        private bool CanUpdate => items != null && items.Length > 0;

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

        private void OnValidate()
        {
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