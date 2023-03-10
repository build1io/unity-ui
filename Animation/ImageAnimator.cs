using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Build1.UnityUI.Animation
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Image))]
    public sealed class ImageAnimator : MonoBehaviour
    {
        [SerializeField] private Image           image;
        [SerializeField] private float           framesPerSecond = 24;
        [SerializeField] private int             startFrame;
        [SerializeField] private bool            loop        = true;
        [SerializeField] private bool            playOnAwake = true;
        [SerializeField] private Sprite[]        sprites;
        [SerializeField] private UnityEvent<int> onFrameChanged = new UnityEvent<int>();
        [SerializeField] private UnityEvent      onComplete     = new UnityEvent();

        public Sprite[] Sprites
        {
            get => sprites;
            set => sprites = value;
        }

        public int  CurrentFrame => _frame;
        public int  TotalFrames  => sprites.Length;
        public bool Reverse      { get; set; }

        public UnityEvent<int> OnFrameChanged
        {
            get => onFrameChanged;
            set => onFrameChanged = value;
        }

        public UnityEvent OnComplete
        {
            get => onComplete;
            set => onComplete = value;
        }

        private int   _frame;
        private float _frameTime;
        private bool  _revalidate;

        /*
         * Mono Behavior.
         */

        private void Awake()
        {
            #if UNITY_EDITOR

            if (!Application.isPlaying)
                return;

            #endif

            image.sprite = sprites[startFrame];
            enabled = playOnAwake;
        }

        #if UNITY_EDITOR

        private double _lastTime;

        private void OnEnable()
        {
            if (!Application.isPlaying)
                EditorApplication.update += EditorUpdate;
        }

        private void OnDisable()
        {
            EditorApplication.update -= EditorUpdate;
        }

        private void Reset()
        {
            if (image == null)
                image = GetComponent<Image>();
        }

        private void OnValidate()
        {
            _revalidate = true;

            if (!Application.isPlaying)
                UpdateStartFrame();
        }

        private void UpdateStartFrame()
        {
            var image = GetComponent<Image>();
            if (image == null)
                return;

            if (sprites == null)
                return;

            startFrame = Mathf.Max(Mathf.Min(startFrame, sprites.Length - 1), 0);
            if (startFrame < sprites.Length)
                image.sprite = sprites[startFrame];
        }

        private void EditorUpdate()
        {
            if (sprites == null || sprites.Length == 0)
                return;

            var deltaTime = EditorApplication.timeSinceStartup - _lastTime;
            _lastTime = EditorApplication.timeSinceStartup;
            ProcessUpdate((float)deltaTime);
        }

        #endif

        private void LateUpdate()
        {
            #if UNITY_EDITOR

            if (!Application.isPlaying)
                return;

            #endif

            ProcessUpdate(Time.deltaTime);
        }

        /*
         * Public.
         */

        public void Play()
        {
            if (_frame == TotalFrames - 1)
                _frame = 0;
            
            enabled = true;
        }
        
        public void Pause() { enabled = false; }

        /*
         * Private.
         */

        private void ProcessUpdate(float deltaTime)
        {
            _frameTime += deltaTime;

            var frameDuration = 1.0F / framesPerSecond;
            while (_frameTime >= frameDuration)
                NextFrame(frameDuration);

            if (!_revalidate || !enabled)
                return;

            image.sprite = sprites[_frame];
            _revalidate = false;
            
            onFrameChanged?.Invoke(_frame);
            
            if (_frame == sprites.Length - 1)
                onComplete?.Invoke();
        }

        private void NextFrame(float frameDuration)
        {
            _frameTime -= frameDuration;

            if (Reverse)
            {
                if (_frame <= 0)
                {
                    if (loop)
                        _frame = sprites.Length - 1;
                    else
                        enabled = false;
                }
                else
                {
                    _frame--;
                }
            }
            else
            {
                if (_frame >= sprites.Length - 1)
                {
                    if (loop)
                        _frame = 0;
                    else
                        enabled = false;
                }
                else
                {
                    _frame++;
                }    
            }
            
            _revalidate = true;
        }
    }
}