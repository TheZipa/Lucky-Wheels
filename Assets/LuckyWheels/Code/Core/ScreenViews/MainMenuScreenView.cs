using System;
using UnityEngine;
using UnityEngine.UI;

namespace LuckyWheels.Code.Core.ScreenViews
{
    public class MainMenuScreenView : Screen
    {
        public event Action OnPlayClick;
        public event Action OnStatisticClick;
        
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _statisticButton;
        
        private void Awake()
        {
            _playButton.onClick.AddListener(SendPlayClick);
            _statisticButton.onClick.AddListener(SendStatisticClick);
        }

        private void SendPlayClick() => OnPlayClick?.Invoke();

        private void SendStatisticClick() => OnStatisticClick?.Invoke();

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(SendPlayClick);
            _statisticButton.onClick.AddListener(SendStatisticClick);
        }
    }
}