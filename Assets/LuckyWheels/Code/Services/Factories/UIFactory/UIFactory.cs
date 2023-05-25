using System.Collections.Generic;
using LuckyWheels.Code.Core.Popup;
using LuckyWheels.Code.Core.ScreenViews;
using LuckyWheels.Code.Core.Statistics;
using LuckyWheels.Code.Core.Wheel;
using LuckyWheels.Code.Data.StaticData.Wheel;
using LuckyWheels.Code.Services.EntityContainer;
using LuckyWheels.Code.Services.Sound;
using LuckyWheels.Code.Services.StaticData;
using UnityEngine;
using Screen = LuckyWheels.Code.Core.ScreenViews.Screen;

namespace LuckyWheels.Code.Services.Factories.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly IStaticData _staticData;
        private readonly IEntityContainer _entityContainer;
        private readonly ISoundService _soundService;

        public UIFactory(IStaticData staticData, IEntityContainer entityContainer, ISoundService soundService)
        {
            _staticData = staticData;
            _entityContainer = entityContainer;
            _soundService = soundService;
        }
        
        public GameObject CreateRootCanvas() => Object.Instantiate(_staticData.Prefabs.RootCanvasPrefab);

        public MainMenuScreenView CreateMainMenuScreen(Transform parent) =>
            CreateScreen(parent, _staticData.Prefabs.MainMenuScreenViewPrefab);

        public CreateWheelScreenView CreateWheelScreenView(Transform parent) =>
            CreateScreen(parent, _staticData.Prefabs.CreateWheelScreenViewPrefab);
        
        public SelectWheelScreenView CreateSelectWheelScreenView(Transform parent) =>
            CreateScreen(parent, _staticData.Prefabs.SelectWheelScreenViewPrefab);
        
        public SpinWheelScreenView CreateSpinWheelScreenView(Transform parent) =>
            CreateScreen(parent, _staticData.Prefabs.SpinWheelScreenViewPrefab);

        public StatisticsScreen CreateStatisticScreen(Transform parent)
        {
            StatisticsScreen statisticsScreen = CreateScreen(parent, _staticData.Prefabs.StatisticsScreenPrefab);
            statisticsScreen.Construct(CreateSpinStatisticViews(statisticsScreen.Content));
            return statisticsScreen;
        }

        public AnimatedHead CreateAnimatedHead(Transform parent)
        {
            AnimatedHead animatedHead = Object.Instantiate(_staticData.Prefabs.AnimatedHeadPrefab, parent);
            animatedHead.Hide();
            _entityContainer.RegisterEntity(animatedHead);
            return animatedHead;
        }

        public WheelView CreateWheel(Transform parent)
        {
            WheelView wheelView = Object.Instantiate(_staticData.Prefabs.WheelViewPrefab, parent);
            wheelView.Construct(CreateWheelSectors(wheelView.SpinWheel));
            wheelView.Hide();
            _entityContainer.RegisterEntity(new Wheel(wheelView, _soundService, _staticData.LuckyWheelsConfig.SpinDuration));
            return wheelView;
        }

        private TScreen CreateScreen<TScreen>(Transform parent, TScreen screenPrefab) where TScreen : Screen
        {
            TScreen screenView = Object.Instantiate(screenPrefab, parent);
            _entityContainer.RegisterEntity(screenView);
            screenView.Hide();
            return screenView;
        }

        private WheelSectorView[] CreateWheelSectors(Transform parent)
        {
            List<WheelSectorView> sectorViews = new List<WheelSectorView>(8);
            WheelData defaultWheelData = _staticData.WheelsData[0];
            float sectorAngle = 360 / defaultWheelData.WheelSectors.Length;

            for (int i = 0; i < defaultWheelData.WheelSectors.Length; i++)
            {
                WheelSectorData wheelSectorData = defaultWheelData.WheelSectors[i];
                WheelSectorView sectorView = Object.Instantiate(_staticData.Prefabs.WheelSectorViewPrefab, parent);
                sectorView.transform.RotateAround(parent.position, Vector3.back, sectorAngle * i);
                sectorView.Construct(wheelSectorData.Icon, wheelSectorData.Label);
                sectorViews.Add(sectorView);
            }

            return sectorViews.ToArray();
        }

        private SpinStatisticView[] CreateSpinStatisticViews(Transform content)
        {
            List<SpinStatisticView> spinStatisticViews = new List<SpinStatisticView>(10);
            for(int i = 0; i < _staticData.LuckyWheelsConfig.SpinStatisticCount; i++)
                spinStatisticViews.Add(Object.Instantiate(_staticData.Prefabs.SpinStatisticViewPrefab, content));
            return spinStatisticViews.ToArray();
        }
    }
}