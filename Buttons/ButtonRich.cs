using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Build1.UnityUIComponents.Buttons
{
    public sealed class ButtonRich : Button
    {
        [SerializeField, FormerlySerializedAs("onDown")]   private UnityEvent             _onDown   = new UnityEvent();
        [SerializeField, FormerlySerializedAs("onUp")]     private UnityEvent             _onUp     = new UnityEvent();
        [SerializeField, FormerlySerializedAs("onOver")]   private UnityEvent             _onOver   = new UnityEvent();
        [SerializeField, FormerlySerializedAs("onOut")]    private UnityEvent             _onOut    = new UnityEvent();
        [SerializeField, FormerlySerializedAs("onAction")] private UnityEvent<ButtonRich> _onAction = new UnityEvent<ButtonRich>();

        public ButtonRichAction CurrentAction         { get; private set; }
        public bool             IsPointerOver         { get; private set; }
        public bool             IsPointerOverDetected { get; private set; }
        public bool             IsPointerDown         { get; private set; }
        public bool             IsPointerDownDetected { get; private set; }
        public bool             IsClickDetected       { get; private set; }

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

        public UnityEvent<ButtonRich> onAction
        {
            get => _onAction;
            set => _onAction = value;
        }

        /*
         * Lifecycle.
         */

        protected override void OnEnable()
        {
            onClick.AddListener(OnClick);
        }

        protected override void OnDisable()
        {
            onClick.RemoveListener(OnClick);
        }

        /*
         * Pointer Handlers.
         */

        public override void OnPointerEnter(PointerEventData eventData)
        {
            CurrentAction = ButtonRichAction.PointerEnter;
            IsPointerOver = true;
            IsPointerOverDetected = true;

            base.OnPointerEnter(eventData);

            _onOver?.Invoke();
            _onAction?.Invoke(this);

            CurrentAction = ButtonRichAction.None;
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            CurrentAction = ButtonRichAction.PointerExit;
            IsPointerOver = false;

            base.OnPointerExit(eventData);

            _onOut?.Invoke();
            _onAction?.Invoke(this);

            CurrentAction = ButtonRichAction.None;
            IsPointerOverDetected = false;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            CurrentAction = ButtonRichAction.PointerDown;
            IsPointerDown = true;
            IsPointerDownDetected = true;

            base.OnPointerDown(eventData);

            _onDown.Invoke();
            _onAction?.Invoke(this);

            CurrentAction = ButtonRichAction.None;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            CurrentAction = ButtonRichAction.PointerUp;
            IsPointerDown = false;

            base.OnPointerUp(eventData);

            _onUp?.Invoke();
            _onAction?.Invoke(this);

            CurrentAction = ButtonRichAction.None;
            IsPointerDownDetected = false;
        }

        /*
         * Event Handlers.
         */

        private void OnClick()
        {
            CurrentAction = ButtonRichAction.Click;
            IsClickDetected = true;

            _onAction?.Invoke(this);

            CurrentAction = ButtonRichAction.None;
            IsClickDetected = false;
        }
    }
}