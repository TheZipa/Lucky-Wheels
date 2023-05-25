using System;
using UnityEngine;
using UnityEngine.UI;

namespace LuckyWheels.Code.Core.Settings
{
    public class SoundSwitcher : MonoBehaviour
    {
        public event Action<bool> OnSoundSwitched;
        
        [SerializeField] private Sprite _soundEnabledSprite;
        [SerializeField] private Sprite _soundDisabledSprite;
        [SerializeField] private Button _soundSwitchButton;

        private bool _isActive;

        public void SetDefault(bool isSoundActive)
        {
            _isActive = isSoundActive;
            SetButtonView();
        }

        private void Awake() =>
            _soundSwitchButton.onClick.AddListener(SwitchSound);

        private void OnDestroy() =>
            _soundSwitchButton.onClick.RemoveListener(SwitchSound);

        private void SwitchSound()
        {
            _isActive = !_isActive;
            SetButtonView();
            OnSoundSwitched?.Invoke(_isActive);
        }

        private void SetButtonView() =>
            _soundSwitchButton.image.sprite = _isActive 
                ? _soundEnabledSprite 
                : _soundDisabledSprite;
    }
}