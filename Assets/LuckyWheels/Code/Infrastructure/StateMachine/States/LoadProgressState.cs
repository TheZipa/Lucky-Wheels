using LuckyWheels.Code.Data.Progress;
using LuckyWheels.Code.Infrastructure.StateMachine.StateSwitcher;
using LuckyWheels.Code.Services.PersistentProgress;
using LuckyWheels.Code.Services.SaveLoad;
using LuckyWheels.Code.Services.Sound;
using LuckyWheels.Code.Services.StaticData;

namespace LuckyWheels.Code.Infrastructure.StateMachine.States
{
    public class LoadProgressState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IPersistentProgress _playerProgress;
        private readonly ISaveLoad _saveLoadService;
        private readonly IStaticData _staticDataService;
        private readonly ISoundService _soundService;

        public LoadProgressState(IStateSwitcher stateSwitcher, IPersistentProgress playerProgress,
            ISaveLoad saveLoadService, IStaticData staticDataService, ISoundService soundService)
        {
            _staticDataService = staticDataService;
            _soundService = soundService;
            _saveLoadService = saveLoadService;
            _playerProgress = playerProgress;
            _stateSwitcher = stateSwitcher;
        }
        
        public void Enter()
        {
            LoadProgressOrInitNew();
            InitializeSoundVolume();
            _stateSwitcher.SwitchTo<LoadPersistentEntityState>();
        }

        public void Exit()
        {
        }
        
        private void LoadProgressOrInitNew() =>
            _playerProgress.Progress = _saveLoadService.LoadProgress() ?? new PlayerProgress();
        
        private void InitializeSoundVolume()
        {
            _soundService.Construct(_staticDataService.SoundData, _playerProgress.Progress.Settings);
        }
    }
}