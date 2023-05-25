using LuckyWheels.Code.Data.Enums;
using LuckyWheels.Code.Services.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace LuckyWheels.Code.Core.Settings
{
    public class SettingsView : MonoBehaviour
    {
        public SoundSettingsView EffectSoundSettingsView;

        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _backgroundCloseButton;

        private ISoundService _soundService;

        public void Construct(Data.Progress.Settings userSettings, ISoundService soundService)
        {
            EffectSoundSettingsView.Construct(userSettings.IsEffectsSoundActive, userSettings.EffectsVolume);
            _soundService = soundService;
        }

        public void Show() => gameObject.SetActive(true);
        
        public void Close()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            gameObject.SetActive(false);
        }

        private void Awake()
        {
            _closeButton.onClick.AddListener(Close);
            _backgroundCloseButton.onClick.AddListener(Close);
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(Close);
            _backgroundCloseButton.onClick.RemoveListener(Close);
        }
    }
}