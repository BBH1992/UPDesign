using Doozy.Runtime.UIManager.Components;
using UnityEngine;
using UnityEngine.UI;

namespace SKODE
{
    public class DoozyUIAudio : MonoBehaviour
    {
        /// <summary>
        /// 若想更改为其他声音,需在Start及之后重新赋值
        /// 当为null时,不会播放声音
        /// </summary>
        [HideInInspector] public AudioClip buttonPressed;

        [HideInInspector] public AudioClip buttonHighlighted;

        [HideInInspector] public AudioClip togglePressed;
        [HideInInspector] public AudioClip toggleHighlighted;

        private Button _button;
        private UIButton _uiButton;

        private Toggle _toggle;
        private UIToggle _uiToggle;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _uiButton = GetComponent<UIButton>();

            _toggle = GetComponent<Toggle>();
            _uiToggle = GetComponent<UIToggle>();

            buttonPressed = DoozyUIAudioManager.Data.buttonPressed;
            buttonHighlighted = DoozyUIAudioManager.Data.buttonHighlighted;

            togglePressed = DoozyUIAudioManager.Data.togglePressed;
            toggleHighlighted = DoozyUIAudioManager.Data.toggleHighlighted;
            
            InitButtonAudio();
            InitUIButtonAudio();

            InitToggleAudio();
            InitUIToggleAudio();
        }
        
        private void InitButtonAudio()
        {
            if (_button == null)
                return;

            _button.onClick.AddListener(delegate
            {
                if (buttonPressed != null)
                    AudioManager.PlayAudioData(new DrAudioData(null, buttonPressed));
            });
        }

        private void InitUIButtonAudio()
        {
            if (_uiButton == null)
                return;

            _uiButton.onClickEvent.AddListener(delegate
            {
                if (buttonPressed != null)
                    AudioManager.PlayAudioData(new DrAudioData(null, buttonPressed));
            });

            _uiButton.onPointerEnterEvent.AddListener(delegate
            {
                if (buttonHighlighted != null)
                    AudioManager.PlayAudioData(new DrAudioData(null, buttonHighlighted));
            });
        }
        
        private void InitToggleAudio()
        {
            if (_toggle == null)
                return;

            _toggle.onValueChanged.AddListener(delegate
            {
                if (togglePressed != null)
                    AudioManager.PlayAudioData(new DrAudioData(null, togglePressed));
            });
        }

        private void InitUIToggleAudio()
        {
            if (_uiToggle == null)
                return;

            _uiToggle.OnValueChangedCallback.AddListener(delegate
            {
                if (togglePressed != null)
                    AudioManager.PlayAudioData(new DrAudioData(null, togglePressed));
            });

            _uiToggle.onPointerEnterEvent.AddListener(delegate
            {
                if (toggleHighlighted != null)
                    AudioManager.PlayAudioData(new DrAudioData(null, toggleHighlighted));
            });
        }
    }
}