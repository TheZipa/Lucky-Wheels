using LuckyWheels.Code.Core.Popup;
using LuckyWheels.Code.Core.ScreenViews;
using LuckyWheels.Code.Core.TopPanel;
using LuckyWheels.Code.Core.Wheel;
using LuckyWheels.Code.Data.Enums;
using LuckyWheels.Code.Data.StaticData.Wheel;
using LuckyWheels.Code.Infrastructure.StateMachine.StateSwitcher;
using LuckyWheels.Code.Services.EntityContainer;
using LuckyWheels.Code.Services.PersistentProgress;
using LuckyWheels.Code.Services.Sound;
using LuckyWheels.Code.Services.StaticData;

namespace LuckyWheels.Code.Infrastructure.StateMachine.States
{
    public class SelectWheelState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IEntityContainer _entityContainer;
        private readonly IStaticData _staticData;
        private readonly IPersistentProgress _persistentProgress;
        private readonly ISoundService _soundService;

        private TopPanelView _topPanelView;
        private SelectWheelScreenView _selectWheelScreenView;
        private int _currentWheelIndex;

        public SelectWheelState(IStateSwitcher stateSwitcher, IEntityContainer entityContainer, 
            IStaticData staticData, IPersistentProgress persistentProgress, ISoundService soundService)
        {
            _stateSwitcher = stateSwitcher;
            _entityContainer = entityContainer;
            _staticData = staticData;
            _persistentProgress = persistentProgress;
            _soundService = soundService;
        }
        
        public void Enter()
        {
            _entityContainer.GetEntity<Wheel>().Hide();
            _entityContainer.GetEntity<AnimatedHead>().Show();
            InitializeTopPanel();
            InitializeWheelSelectScreen();
        }

        public void Exit()
        {
            _topPanelView.HideTitle();
            _topPanelView.OnBackClick -= ReturnToChoiceState;
            _selectWheelScreenView.Hide();
            _selectWheelScreenView.OnSelectClick -= SwitchToSpinOrCreateWheel;
            _selectWheelScreenView.OnNextClick -= SwitchToNextWheel;
            _selectWheelScreenView.OnPreviousClick -= SwitchToPreviousWheel;
        }

        private void InitializeWheelSelectScreen()
        {
            _selectWheelScreenView = _entityContainer.GetEntity<SelectWheelScreenView>();
            _selectWheelScreenView.OnNextClick += SwitchToNextWheel;
            _selectWheelScreenView.OnPreviousClick += SwitchToPreviousWheel;
            _selectWheelScreenView.OnSelectClick += SwitchToSpinOrCreateWheel;
            _selectWheelScreenView.Show();
        }

        private void InitializeTopPanel()
        {
            _topPanelView = _entityContainer.GetEntity<TopPanelView>();
            _topPanelView.ShowBackButton();
            _topPanelView.ShowTitle("Choose");
            _topPanelView.OnBackClick += ReturnToChoiceState;
        }

        private void SwitchToPreviousWheel()
        {
            _currentWheelIndex = _currentWheelIndex - 1 < 0 
                ? _staticData.WheelsData.Length - 1 
                : _currentWheelIndex - 1;
            SetNewWheelPreview();
        }

        private void SwitchToNextWheel()
        {
            _currentWheelIndex = _currentWheelIndex + 1 == _staticData.WheelsData.Length
                ? _currentWheelIndex = 0
                : _currentWheelIndex + 1;
            SetNewWheelPreview();
        }

        private void SetNewWheelPreview()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            WheelData currentWheelData = _staticData.WheelsData[_currentWheelIndex];
            _selectWheelScreenView.SetNewWheelPreview(currentWheelData.ChoicePreview);
        }

        private void ReturnToChoiceState() => _stateSwitcher.SwitchTo<MenuState>();

        private void SwitchToSpinOrCreateWheel()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            WheelData currentWheelData = _staticData.WheelsData[_currentWheelIndex];
            _persistentProgress.CurrentWheelIndex = _currentWheelIndex;
            if (currentWheelData.IsCustom)
            {
                _stateSwitcher.SwitchTo<CreateWheelState>();
            }
            else
            {
                _entityContainer.GetEntity<Wheel>().Configure(currentWheelData);
                _stateSwitcher.SwitchTo<SpinWheelState>(typeof(SelectWheelState));
            }
            _entityContainer.GetEntity<SpinWheelScreenView>().Configure(currentWheelData.Head, currentWheelData.Question);
        }
    }
}