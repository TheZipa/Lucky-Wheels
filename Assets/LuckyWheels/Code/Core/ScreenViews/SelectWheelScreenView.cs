using System;
using UnityEngine;
using UnityEngine.UI;

namespace LuckyWheels.Code.Core.ScreenViews
{
    public class SelectWheelScreenView : Screen
    {
        public event Action OnSelectClick;
        public event Action OnPreviousClick;
        public event Action OnNextClick;
        
        [SerializeField] private Button _selectButton;
        [SerializeField] private Button _previousWheelButton;
        [SerializeField] private Button _nextWheelButton;
        [SerializeField] private Image _choicePreviewImage;

        public void SetNewWheelPreview(Sprite preview) => _choicePreviewImage.sprite = preview;

        private void Start()
        {
            _selectButton.onClick.AddListener(SendSelectClick);
            _nextWheelButton.onClick.AddListener(SendNextClick);
            _previousWheelButton.onClick.AddListener(SendPreviousClick);
        }

        private void SendSelectClick() => OnSelectClick?.Invoke();

        private void SendPreviousClick() => OnPreviousClick?.Invoke();

        private void SendNextClick() => OnNextClick?.Invoke();

        private void OnDestroy()
        {
            _selectButton.onClick.RemoveListener(SendSelectClick);
            _nextWheelButton.onClick.RemoveListener(SendNextClick);
            _previousWheelButton.onClick.RemoveListener(SendPreviousClick);
        }
    }
}