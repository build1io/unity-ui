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

        private void Awake()
        {
            UnityUI.OnInterfaceTypeChanged += UpdateAspectRatioFitter;
        }

        private void OnEnable()
        {
            UpdateAspectRatioFitter(UnityUI.CurrentInterfaceType);
        }
        
        private void OnDestroy()
        {
            UnityUI.OnInterfaceTypeChanged -= UpdateAspectRatioFitter;
        }

        #if UNITY_EDITOR

        private void Reset()
        {
            if (aspectRatioFitter == null)
                aspectRatioFitter = GetComponent<AspectRatioFitter>();

            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();
        }

        #endif

        private void UpdateAspectRatioFitter(InterfaceType currentInterfaceType)
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