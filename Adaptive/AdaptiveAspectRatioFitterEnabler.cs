using Build1.UnityUI.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Build1.UnityUI.Adaptive
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AspectRatioFitter))]
    [RequireComponent(typeof(RectTransform))]
    public sealed class AdaptiveAspectRatioFitterEnabler : MonoBehaviour
    {
        [SerializeField] public AspectRatioFitter aspectRatioFitter;
        [SerializeField] public RectTransform     rectTransform;
        [SerializeField] public InterfaceType     interfaceType;
        [SerializeField] public bool              stretch = true;
        [SerializeField] public bool              resetOffsetsWhenAspectRationFitterDisabled = true;

        private bool CanUpdate => aspectRatioFitter != null && rectTransform != null;
        
        private void Awake()
        {
            if (Application.isPlaying)
                ScreenUtil.SubscribeOnScreenResolutionChanged(UpdateAspectRatioFitter);

            UpdateAspectRatioFitter();
        }

        private void OnDestroy()
        {
            if (Application.isPlaying)
                ScreenUtil.UnsubscribeFromScreenResolutionChanged(UpdateAspectRatioFitter);
        }

        #if UNITY_EDITOR

        private InterfaceType _interfaceType;

        private void Update()
        {
            if (!CanUpdate)
                return;
            
            var currentInterfaceType = InterfaceUtil.GetInterfaceType(Application.platform, SystemInfo.deviceType);
            if (currentInterfaceType == _interfaceType)
                return;

            _interfaceType = currentInterfaceType;
            UpdateAspectRatioFitterImpl(currentInterfaceType);
        }
        
        private void Reset()
        {
            if (aspectRatioFitter == null)
                aspectRatioFitter = GetComponent<AspectRatioFitter>();

            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();
        }

        private void OnValidate()
        {
            EditorApplication.update += OnValidateImpl;
        }

        private void OnValidateImpl()
        {
            EditorApplication.update -= OnValidateImpl;
            UpdateAspectRatioFitter();
        }

        #endif

        private void UpdateAspectRatioFitter()
        {
            if (!CanUpdate)
                return;
            
            var currentInterfaceType = InterfaceUtil.GetInterfaceType(Application.platform, SystemInfo.deviceType);
            UpdateAspectRatioFitterImpl(currentInterfaceType);
        }

        private void UpdateAspectRatioFitterImpl(InterfaceType currentInterfaceType)
        {
            aspectRatioFitter.enabled = (interfaceType & currentInterfaceType) == currentInterfaceType;

            if (stretch)
            {
                rectTransform.anchorMin = new Vector2(0, 0);
                rectTransform.anchorMax = new Vector2(1, 1);
            }

            if (!aspectRatioFitter.enabled && resetOffsetsWhenAspectRationFitterDisabled)
            {
                rectTransform.offsetMin = Vector2.zero;
                rectTransform.offsetMax = Vector2.zero;
            }
        }
    }
}