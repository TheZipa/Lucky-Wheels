using LuckyWheels.Code.Data.StaticData.Wheel;
using UnityEngine;
using UnityEngine.UI;

namespace LuckyWheels.Code.Core.Wheel
{
    public class WheelView : MonoBehaviour
    {
        public Transform SpinWheel;

        [SerializeField] private Image _spinWheelImage;
        [SerializeField] private float _iPadYPosition;
        [SerializeField] private float _iPhoneYPosition;
        [SerializeField] private RectTransform _rect;
        private WheelSectorView[] _wheelSectors;

        public void Construct(WheelSectorView[] wheelSectors)
        {
            _wheelSectors = wheelSectors;
            bool isIPad = Screen.height > 1500;
            _rect.anchoredPosition = new Vector2(0, isIPad ? _iPadYPosition : _iPhoneYPosition);
            if (isIPad) _rect.sizeDelta = new Vector2(1200, 1200);
        }

        public void Configure(WheelData wheelData)
        {
            _spinWheelImage.sprite = wheelData.Wheel;
            for (int i = 0; i < _wheelSectors.Length; i++)
            {
                WheelSectorData wheelSectorData = wheelData.WheelSectors[i];
                _wheelSectors[i].Construct(wheelSectorData.Icon, wheelSectorData.Label);
            }
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }
}