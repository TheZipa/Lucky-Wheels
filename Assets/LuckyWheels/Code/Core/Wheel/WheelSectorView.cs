using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LuckyWheels.Code.Core.Wheel
{
    public class WheelSectorView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _label;

        public void Construct(Sprite icon, string label)
        {
            SetIcon(icon);
            SetLabel(label);
        }

        private void SetIcon(Sprite icon)
        {
            if (icon == null)
            {
                _icon.gameObject.SetActive(false);
            }
            else
            {
                _icon.sprite = icon;
                _icon.gameObject.SetActive(true);
            }
        }

        private void SetLabel(string label)
        {
            if (String.IsNullOrWhiteSpace(label))
            {
                _label.gameObject.SetActive(false);
            }
            else
            {
                _label.text = label;
                _label.gameObject.SetActive(true);
            }
        }
    }
}