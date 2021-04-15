using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Build1.PostMVC.Extensions.Unity.Components
{
    public sealed class ButtonComplex : Button
    {
        public readonly UnityEvent onUp   = new UnityEvent();
        public readonly UnityEvent onDown = new UnityEvent();
        public readonly UnityEvent onOver  = new UnityEvent();
        public readonly UnityEvent onOut  = new UnityEvent();
        
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
         
            onOver?.Invoke();
        }
        
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            
            onOut?.Invoke();
        }
        
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            
            onDown.Invoke();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            
            onUp?.Invoke();
        }
    }
}