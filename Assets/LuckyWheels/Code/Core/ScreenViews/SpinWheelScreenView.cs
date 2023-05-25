using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LuckyWheels.Code.Core.ScreenViews
{
    public class SpinWheelScreenView : Screen
    {
        public event Action OnSpinClick;
        
        [SerializeField] private Button _spinButton;
        [SerializeField] private Image _headImage;
        [SerializeField] private TextMeshProUGUI _questionText;

        public void Configure(Sprite head, string question)
        {
            _headImage.sprite = head;
            _questionText.text = question;
        }
        
        public void EnableSpinButton() => _spinButton.interactable = true;
        
        public void DisableSpinButton() => _spinButton.interactable = false;

        private void Start() => _spinButton.onClick.AddListener(SendSpinClick);

        private void SendSpinClick() => OnSpinClick?.Invoke();

        private void OnDestroy() => _spinButton.onClick.RemoveListener(SendSpinClick);
    }
}