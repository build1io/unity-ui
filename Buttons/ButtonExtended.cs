using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Build1.UnityUIComponents.Buttons
{
    [RequireComponent(typeof(Image))]
    public sealed class ButtonExtended : Button
    {
        [SerializeField, FormerlySerializedAs("onDown")] private UnityEvent _onDown = new UnityEvent();
        [SerializeField, FormerlySerializedAs("onUp")]   private UnityEvent _onUp   = new UnityEvent();
        [SerializeField, FormerlySerializedAs("onOver")] private UnityEvent _onOver = new UnityEvent();
        [SerializeField, FormerlySerializedAs("onOut")]  private UnityEvent _onOut  = new UnityEvent();

        public UnityEvent onDown
        {
            get => _onDown;
            set => _onDown = value;
        }
        
        public UnityEvent onUp
        {
            get => _onUp;
            set => _onUp = value;
        }
        
        public UnityEvent onOver
        {
            get => _onOver;
            set => _onOver = value;
        }
        
        public UnityEvent onOut
        {
            get => _onOut;
            set => _onOut = value;
        }
        
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            _onOver?.Invoke();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            _onOut?.Invoke();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            _onDown.Invoke();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            _onUp?.Invoke();
        }
    }
}