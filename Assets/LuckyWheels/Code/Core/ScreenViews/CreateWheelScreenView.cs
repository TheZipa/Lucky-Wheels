using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LuckyWheels.Code.Core.ScreenViews
{
    public class CreateWheelScreenView : Screen
    {
        public event Action<string> OnNextClick;
        public event Action OnPlayClick;

        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _playButton;
        [SerializeField] private TMP_InputField _sectorInputField;

        public void ActiveNextButton(bool isActive) => _nextButton.interactable = isActive;

        public void ActivePlayButton(bool isActive) => _playButton.interactable = isActive;
        
        private void Start()
        {
            _playButton.onClick.AddListener(SendCreateClick);
            _nextButton.onClick.AddListener(SendAddSector);
        }

        private void SendCreateClick() => OnPlayClick?.Invoke();

        private void SendAddSector()
        {
            if (String.IsNullOrEmpty(_sectorInputField.text)) return;
            OnNextClick?.Invoke(_sectorInputField.text);
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(SendCreateClick);
            _nextButton.onClick.RemoveListener(SendAddSector);
        }
    }
}