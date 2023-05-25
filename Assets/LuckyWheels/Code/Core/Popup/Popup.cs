using System;
using System.Collections;
using LuckyWheels.Code.Data.Enums;
using LuckyWheels.Code.Data.StaticData.Wheel;
using LuckyWheels.Code.Services.Sound;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LuckyWheels.Code.Core.Popup
{
    public class Popup : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _backgroundCloseButton;
        [SerializeField] private Image _resultIcon;
        [SerializeField] private TextMeshProUGUI _labelText;

        private ISoundService _soundService;

        public void Construct(ISoundService soundService) => _soundService = soundService;

        public void Show(WheelSectorData wheelSectorData)
        {
            SetIcon(wheelSectorData.Icon);
            SetLabel(wheelSectorData.Label);

            gameObject.SetActive(true);
            StartCoroutine(FadeShow());
            _soundService.PlayEffectSound(SoundId.Popup);
        }

        private void Start()
        {
            _closeButton.onClick.AddListener(Close);
            _backgroundCloseButton.onClick.AddListener(Close);
            _canvasGroup.alpha = 0f;
            gameObject.SetActive(false);
        }

        private void Close()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            _canvasGroup.alpha = 0f;
            gameObject.SetActive(false);
        }

        private void SetIcon(Sprite icon)
        {
            if (icon == null)
            {
                _resultIcon.gameObject.SetActive(false);
            }
            else
            {
                _resultIcon.sprite = icon;
                _resultIcon.gameObject.SetActive(true);
            }
        }

        private void SetLabel(string label)
        {
            if (String.IsNullOrWhiteSpace(label))
            {
                _labelText.gameObject.SetActive(false);
            }
            else
            {
                _labelText.text = label;
                _labelText.gameObject.SetActive(true);
            }
        }

        private IEnumerator FadeShow()
        {
            while (_canvasGroup.alpha < 1f)
            {
                _canvasGroup.alpha += 0.025f;
                yield return new WaitForSeconds(0.01f);
            }
            _canvasGroup.alpha = 1f;
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(Close);
            _backgroundCloseButton.onClick.RemoveListener(Close);
        }
    }
}