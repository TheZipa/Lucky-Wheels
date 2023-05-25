using System;
using DG.Tweening;
using LuckyWheels.Code.Data.Enums;
using LuckyWheels.Code.Data.StaticData.Wheel;
using LuckyWheels.Code.Services.Sound;
using UnityEngine;

namespace LuckyWheels.Code.Core.Wheel
{
    public class Wheel
    {
        public event Action<WheelSectorData> OnSpinFinished;
        
        private readonly WheelView _view;
        private readonly ISoundService _soundService;
        private readonly int _spinDuration;
        private readonly System.Random _random = new System.Random();

        private WheelSectorData[] _wheelSectorsData;
        private float _sectorAngle;
        private float _halfSectorAngle;
        private float _halfSectorAngleWithPaddings;
        private double _accumulatedWeight;

        public Wheel(WheelView view, ISoundService soundService, int spinDuration)
        {
            _view = view;
            _soundService = soundService;
            _spinDuration = spinDuration;
        }

        public void Configure(WheelData wheelData)
        {
            _wheelSectorsData = wheelData.WheelSectors;
            _view.Configure(wheelData);
            CalculateSectorAngles();
        }

        public void Spin()
        {
            int index = GetRandomSectorIndex();
            WheelSectorData retrievedSector = _wheelSectorsData[index];
            Vector3 targetRotation = GetTargetRotation(index);
            RotateWheelView(targetRotation, () => OnSpinFinished?.Invoke(retrievedSector));
        }

        public void Show() => _view.Show();

        public void Hide() => _view.Hide();
        
        private void RotateWheelView(Vector3 targetRotation, Action OnViewStopped = null)
        {
            float currentAngle;
            float previousAngle = currentAngle = _view.SpinWheel.eulerAngles.z;
            bool isIndicatorOnTheLine = false;

            _view.SpinWheel.DORotate(targetRotation, _spinDuration, RotateMode.FastBeyond360).SetEase(Ease.InOutQuart)
                .OnUpdate(() =>
                {
                    float diff = Mathf.Abs(previousAngle - currentAngle);
                    if (diff >= _halfSectorAngle)
                    {
                        if (isIndicatorOnTheLine) _soundService.PlayEffectSound(SoundId.Spin);
                        previousAngle = currentAngle;
                        isIndicatorOnTheLine = !isIndicatorOnTheLine;
                    }

                    currentAngle = _view.SpinWheel.eulerAngles.z;
                }).OnComplete(() => OnViewStopped?.Invoke());
        }

        private Vector3 GetTargetRotation(int sectorIndex)
        {
            float angle = -(_sectorAngle * sectorIndex) - 180;
            return Vector3.back * (angle + 2 * 360 * _spinDuration);
        }

        private void CalculateSectorAngles()
        {
            _sectorAngle = 360 / _wheelSectorsData.Length;
            _halfSectorAngle = _sectorAngle / 2f;
            _halfSectorAngleWithPaddings = _halfSectorAngle - _halfSectorAngle / 4f;
        }

        private int GetRandomSectorIndex() => _random.Next(0, _wheelSectorsData.Length);
    }
}