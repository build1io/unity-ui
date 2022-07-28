using System;
using TMPro;
using UnityEngine;

namespace Build1.UnityUI.TextMeshPro
{
    [ExecuteAlways]
    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class TextMeshProContentSizeFitter : MonoBehaviour
    {
        public enum FitMode
        {
            Unconstrained,
            MinSize,
            PreferredSize
        }

        [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
        [SerializeField] private FitMode         _horizontalFit = FitMode.Unconstrained;
        [SerializeField] private float           _widthMin;
        [SerializeField] private float           _widthMax;
        [SerializeField] private FitMode         _verticalFit = FitMode.Unconstrained;
        [SerializeField] private float           _heightMin;
        [SerializeField] private float           _heightMax;

        public FitMode HorizontalFit => _horizontalFit;
        public FitMode VerticalFit   => _verticalFit;

        private void OnEnable()
        {
            #if !UNITY_EDITOR
            _textMeshProUGUI.RegisterDirtyLayoutCallback(UpdateRectTransform);
            #endif

            UpdateRectTransform();
        }

        private void OnDisable()
        {
            #if !UNITY_EDITOR
            _textMeshProUGUI.UnregisterDirtyLayoutCallback(UpdateRectTransform);
            #endif
        }

        private void OnDestroy()
        {
            _textMeshProUGUI.UnregisterDirtyLayoutCallback(UpdateRectTransform);
            _textMeshProUGUI = null;
        }

        /*
         * Mono Behavior.
         */

        #if UNITY_EDITOR

        [NonSerialized] private Vector2 _lastSize; 
        
        private void Awake()
        {
            if (_textMeshProUGUI != null)
                return;
            
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            
            _widthMin = _textMeshProUGUI.minWidth;
            _widthMax = _textMeshProUGUI.preferredWidth;
            
            _heightMin = _textMeshProUGUI.minHeight;
            _heightMax = _textMeshProUGUI.preferredHeight;
        }

        private void Update()
        {
            if (_lastSize.x == _textMeshProUGUI.preferredWidth && _lastSize.y == _textMeshProUGUI.preferredHeight)
                return;
            
            _lastSize = new Vector2(_textMeshProUGUI.preferredWidth, _textMeshProUGUI.preferredHeight);
            UpdateRectTransform();
        }

        private void OnValidate()
        {
            UnityEditor.EditorApplication.delayCall += OnValidateImpl;
        }

        private void OnValidateImpl()
        {
            if (_textMeshProUGUI != null)
                UpdateRectTransform();
        }

        #endif

        /*
         * Private.
         */

        private void UpdateRectTransform()
        {
            _textMeshProUGUI.rectTransform.sizeDelta = new Vector2
            {
                x = _horizontalFit switch
                {
                    FitMode.Unconstrained => _textMeshProUGUI.rectTransform.sizeDelta.x,
                    FitMode.MinSize       => Mathf.Max(Mathf.Min(_textMeshProUGUI.minWidth, _widthMax), _widthMin),
                    FitMode.PreferredSize => Mathf.Max(Mathf.Min(_textMeshProUGUI.preferredWidth, _widthMax), _widthMin),
                    _                     => throw new ArgumentOutOfRangeException()
                },
                y = _verticalFit switch
                {
                    FitMode.Unconstrained => _textMeshProUGUI.rectTransform.sizeDelta.y,
                    FitMode.MinSize       => Mathf.Max(Mathf.Min(_textMeshProUGUI.minHeight, _heightMax), _heightMin),
                    FitMode.PreferredSize => Mathf.Max(Mathf.Min(_textMeshProUGUI.preferredHeight, _heightMax), _heightMin),
                    _                     => throw new ArgumentOutOfRangeException()
                }
            };
        }
    }
}