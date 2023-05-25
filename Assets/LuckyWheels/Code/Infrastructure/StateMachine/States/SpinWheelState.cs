using System;
using LuckyWheels.Code.Core.Popup;
using LuckyWheels.Code.Core.ScreenViews;
using LuckyWheels.Code.Core.TopPanel;
using LuckyWheels.Code.Core.Wheel;
using LuckyWheels.Code.Data;
using LuckyWheels.Code.Data.Enums;
using LuckyWheels.Code.Data.StaticData.Wheel;
using LuckyWheels.Code.Infrastructure.StateMachine.StateSwitcher;
using LuckyWheels.Code.Services.EntityContainer;
using LuckyWheels.Code.Services.PersistentProgress;
using LuckyWheels.Code.Services.SaveLoad;
using LuckyWheels.Code.Services.Sound;

namespace LuckyWheels.Code.Infrastructure.StateMachine.States
{
    public class SpinWheelState : IPayloadedState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IEntityContainer _entityContainer;
        private readonly ISoundService _soundService;
        private readonly IPersistentProgress _persistentProgress;
        private readonly ISaveLoad _saveLoad;

        private SpinWheelScreenView _spinWheelScreenView;
        private TopPanelView _topPanelView;
        private Wheel _wheel;
        private Type _returnState;

        public SpinWheelState(IStateSwitcher stateSwitcher, IEntityContainer entityContainer, ISoundService soundService,
            IPersistentProgress persistentProgress, ISaveLoad saveLoad)
        {
            _stateSwitcher = stateSwitcher;
            _entityContainer = entityContainer;
            _soundService = soundService;
            _persistentProgress = persistentProgress;
            _saveLoad = saveLoad;
        }
        
        public void Enter(object payload)
        {
            _returnState = (Type)payload;
            _entityContainer.GetEntity<AnimatedHead>().Hide();
            InitializeWheel();
            InitializeView();
        }

        public void Exit()
        {
            _spinWheelScreenView.Hide();
            _topPanelView.OnBackClick -= ReturnToPreviousState;
            _wheel.Hide();
            _wheel.OnSpinFinished -= OnSpinFinished;
            _spinWheelScreenView.OnSpinClick -= SpinWheel;
        }

        private void InitializeView()
        {
            _topPanelView = _entityContainer.GetEntity<TopPanelView>();
            _topPanelView.OnBackClick += ReturnToPreviousState;
            _spinWheelScreenView = _entityContainer.GetEntity<SpinWheelScreenView>();
            _spinWheelScreenView.Show();
            _spinWheelScreenView.OnSpinClick += SpinWheel;
        }

        private void InitializeWheel()
        {
            _wheel = _entityContainer.GetEntity<Wheel>();
            _wheel.OnSpinFinished += OnSpinFinished;
            _wheel.Show();
        }

        private void SpinWheel()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            _topPanelView.HideBackButton();
            _spinWheelScreenView.DisableSpinButton();
            _wheel.Spin();
        }

        private void OnSpinFinished(WheelSectorData wheelSectorData)
        {
            SaveSpinHistory(wheelSectorData.Label);
            _topPanelView.ShowBackButton();
            _spinWheelScreenView.EnableSpinButton();
            _entityContainer.GetEntity<Popup>().Show(wheelSectorData);
        }

        private void SaveSpinHistory(string resultSector)
        {
            _persistentProgress.AddSpinHistory(_persistentProgress.CurrentWheelIndex, resultSector);
            _saveLoad.SaveProgress();
        }

        private void ReturnToPreviousState() => _stateSwitcher.SwitchTo(_returnState);
    }
}