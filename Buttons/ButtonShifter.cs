using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Build1.UnityUIComponents.Buttons
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public sealed class ButtonShifter : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] public Vector2      offsetPixels;
        [SerializeField] public GameObject[] objects;

        private Button _button;
        private bool   _downRegistered;
        
        public void Awake()
        {
            _button = GetComponent<Button>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_button.interactable)
                return;
            
            var offset = -offsetPixels * transform.lossyScale;
            foreach (var obj in objects)
                obj.transform.Translate(offset);

            _downRegistered = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_downRegistered)
                return;
            
            var offset = offsetPixels * transform.lossyScale;
            foreach (var obj in objects)
                obj.transform.Translate(offset);
            
            _downRegistered = false;
        }
    }
}