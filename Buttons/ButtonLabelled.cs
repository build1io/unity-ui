using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Build1.UnityUIComponents.Buttons
{
    [RequireComponent(typeof(Image))]
    public sealed class ButtonLabelled : Button
    {
        [SerializeField, FormerlySerializedAs("label")]            private TextMeshProUGUI _label;
        [SerializeField, FormerlySerializedAs("colorNormal")]      private Color           _colorNormal      = Color.white;
        [SerializeField, FormerlySerializedAs("colorHighlighted")] private Color           _colorHighlighted = Color.white;
        [SerializeField, FormerlySerializedAs("colorPressed")]     private Color           _colorPressed     = Color.white;
        [SerializeField, FormerlySerializedAs("colorSelected")]    private Color           _colorSelected    = Color.white;
        [SerializeField, FormerlySerializedAs("colorDisabled")]    private Color           _colorDisabled    = Color.white;

        public string Label
        {
            get => _label.text;
            set => _label.text = value;
        }
        
        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            if (!gameObject.activeInHierarchy)
                return;
            
            base.DoStateTransition(state, instant);

            if (_label == null)
                return;
            
            _label.color = state switch
            {
                SelectionState.Normal      => _colorNormal,
                SelectionState.Highlighted => _colorHighlighted,
                SelectionState.Pressed     => _colorPressed,
                SelectionState.Selected    => _colorSelected,
                SelectionState.Disabled    => _colorDisabled,
                _                          => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            };
        }
    }
}