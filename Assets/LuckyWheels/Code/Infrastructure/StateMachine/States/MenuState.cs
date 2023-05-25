using LuckyWheels.Code.Core.Popup;
using LuckyWheels.Code.Core.ScreenViews;
using LuckyWheels.Code.Core.TopPanel;
using LuckyWheels.Code.Data.Enums;
using LuckyWheels.Code.Infrastructure.StateMachine.StateSwitcher;
using LuckyWheels.Code.Services.EntityContainer;
using LuckyWheels.Code.Services.Sound;

namespace LuckyWheels.Code.Infrastructure.StateMachine.States
{
    public class MenuState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IEntityContainer _entityContainer;
        private readonly ISoundService _soundService;

        private MainMenuScreenView _mainMenuScreenView;

        public MenuState(IStateSwitcher stateSwitcher, IEntityContainer entityContainer, ISoundService soundService)
        {
            _stateSwitcher = stateSwitcher;
            _entityContainer = entityContainer;
            _soundService = soundService;
        }

        public void Enter()
        {
            _entityContainer.GetEntity<TopPanelView>().HideBackButton();
            _entityContainer.GetEntity<AnimatedHead>().Hide();
            _mainMenuScreenView = _entityContainer.GetEntity<MainMenuScreenView>();
            _mainMenuScreenView.OnStatisticClick += SwitchToStatisticScreen;
            _mainMenuScreenView.OnPlayClick += SwitchToSelectWheel;
            _mainMenuScreenView.Show();
        }

        public void Exit()
        {
            _mainMenuScreenView.Hide();
            _mainMenuScreenView.OnPlayClick -= SwitchToSelectWheel;
            _mainMenuScreenView.OnStatisticClick -= SwitchToStatisticScreen;
        }

        private void SwitchToSelectWheel()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            _stateSwitcher.SwitchTo<SelectWheelState>();
        }

        private void SwitchToStatisticScreen()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            _stateSwitcher.SwitchTo<StatisticState>();
        }
    }
}