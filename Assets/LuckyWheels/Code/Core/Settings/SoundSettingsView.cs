using System;
using UnityEngine;
using UnityEngine.UI;

namespace LuckyWheels.Code.Core.Settings
{
    public class SoundSettingsView : MonoBehaviour
    {
        public event Action<bool> OnSwitch;
        public event Action<float> OnVolumeChanged;
        
        [SerializeField] private SoundSwitcher _soundSwitcher;
        [SerializeField] private Slider _volumeSlider;

        private void Awake()
        {
            _soundSwitcher.OnSoundSwitched += OnSoundSwitch;
            _volumeSlider.onValueChanged.AddListener(SendVolumeChange);
        }

        public void Construct(bool isActive, float volume)
        {
            _soundSwitcher.SetDefault(isActive);
            _volumeSlider.interactable = isActive;
            _volumeSlider.value = volume;
        }

        private void OnSoundSwitch(bool isActive)
        {
            _volumeSlider.interactable = isActive;
            OnSwitch?.Invoke(isActive);
        }

        private void SendVolumeChange(float volume) =>
            OnVolumeChanged?.Invoke(volume);

        private void OnDestroy()
        {
            _soundSwitcher.OnSoundSwitched -= OnSoundSwitch;
            _volumeSlider.onValueChanged.RemoveListener(SendVolumeChange);
        }
    }
}