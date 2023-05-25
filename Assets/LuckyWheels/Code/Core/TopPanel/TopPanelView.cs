using System;
using LuckyWheels.Code.Core.Settings;
using LuckyWheels.Code.Data.Enums;
using LuckyWheels.Code.Services.Sound;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LuckyWheels.Code.Core.TopPanel
{
    public class TopPanelView : MonoBehaviour
    {
        public event Action OnBackClick;

        [SerializeField] private Button _backButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private TextMeshProUGUI _titleText;

        private SettingsView _settingsView;
        private ISoundService _soundService;

        public void Construct(SettingsView settingsView, ISoundService soundService)
        {
            _settingsView = settingsView;
            _soundService = soundService;
        }

        public void HideBackButton() => _backButton.gameObject.SetActive(false);

        public void ShowBackButton() => _backButton.gameObject.SetActive(true);

        public void ShowTitle(string titleText)
        {
            _titleText.gameObject.SetActive(true);
            _titleText.text = titleText;
        }

        public void HideTitle() => _titleText.gameObject.SetActive(false);

        private void Start()
        {
            _backButton.onClick.AddListener(SendBackClick);
            _settingsButton.onClick.AddListener(ShowSettingsView);
        }

        private void SendBackClick()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            OnBackClick?.Invoke();
        }

        private void ShowSettingsView()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            _settingsView.Show();
        }

        private void OnDestroy()
        {
            _backButton.onClick.RemoveListener(SendBackClick);
            _settingsButton.onClick.RemoveListener(ShowSettingsView);
        }
    }
}