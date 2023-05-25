using System;
using System.Collections.Generic;
using System.Linq;
using LuckyWheels.Code.Core.Popup;
using LuckyWheels.Code.Core.ScreenViews;
using LuckyWheels.Code.Core.TopPanel;
using LuckyWheels.Code.Core.Wheel;
using LuckyWheels.Code.Data;
using LuckyWheels.Code.Data.Enums;
using LuckyWheels.Code.Data.StaticData.Wheel;
using LuckyWheels.Code.Infrastructure.StateMachine.StateSwitcher;
using LuckyWheels.Code.Services.EntityContainer;
using LuckyWheels.Code.Services.Sound;
using LuckyWheels.Code.Services.StaticData;

namespace LuckyWheels.Code.Infrastructure.StateMachine.States
{
    public class CreateWheelState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IEntityContainer _entityContainer;
        private readonly ISoundService _soundService;
        private readonly WheelData _customWheelData;
        private readonly List<string> _sectorContents = new List<string>(MaxSectorsCount);

        private const int MaxSectorsCount = 10;
        private TopPanelView _topPanelView;
        private Wheel _wheel;
        private CreateWheelScreenView _createWheelScreenView;

        public CreateWheelState(IStateSwitcher stateSwitcher, IEntityContainer entityContainer, 
            IStaticData staticData, ISoundService soundService)
        {
            _stateSwitcher = stateSwitcher;
            _entityContainer = entityContainer;
            _soundService = soundService;
            _customWheelData = staticData.WheelsData.FirstOrDefault(wheelData => wheelData.IsCustom);
        }
        
        public void Enter()
        {
            _topPanelView = _entityContainer.GetEntity<TopPanelView>();
            _topPanelView.OnBackClick += ReturnToChoiceState;
            _entityContainer.GetEntity<AnimatedHead>().Show();
            
            InitializeCreateWheelScreen();
            InitializeWheel();
        }
        
        public void Exit()
        {
            _createWheelScreenView.Hide();
            _topPanelView.OnBackClick -= ReturnToChoiceState;
            _createWheelScreenView.OnPlayClick -= SwitchToSpinState;
            _createWheelScreenView.OnNextClick -= AddSector;
        }

        private void InitializeCreateWheelScreen()
        {
            _createWheelScreenView = _entityContainer.GetEntity<CreateWheelScreenView>();
            _createWheelScreenView.OnPlayClick += SwitchToSpinState;
            _createWheelScreenView.OnNextClick += AddSector;
            _createWheelScreenView.Show();
        }
        
        private void InitializeWheel()
        {
            _wheel = _entityContainer.GetEntity<Wheel>();
            foreach (WheelSectorData wheelSector in _customWheelData.WheelSectors) 
                wheelSector.Label = String.Empty;
            _wheel.Configure(_customWheelData);
            _wheel.Show();
        }

        private void AddSector(string sectorText)
        {
            _soundService.PlayEffectSound(SoundId.Click);
            _sectorContents.Add(sectorText);
            if(_sectorContents.Count == MaxSectorsCount) _createWheelScreenView.ActiveNextButton(false);
            UpdateWheelSectors();
        }

        private void UpdateWheelSectors()
        {
            if (IsSectorCountValid(_sectorContents.Count))
            {
                DefineCustomWheelSectors();
                _wheel.Configure(_customWheelData);
                _createWheelScreenView.ActivePlayButton(true);
            }
            else
            {
                _createWheelScreenView.ActivePlayButton(false);
            }
        }

        private bool IsSectorCountValid(int sectorCount) => MaxSectorsCount % sectorCount == 0;

        private void DefineCustomWheelSectors()
        {
            int sectorPointer = 0;
            foreach (WheelSectorData wheelSector in _customWheelData.WheelSectors)
            {
                wheelSector.Label = _sectorContents[sectorPointer];
                if (++sectorPointer == _sectorContents.Count) sectorPointer = 0;
            }
        }

        private void SwitchToSpinState()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            _entityContainer.GetEntity<Wheel>().Configure(_customWheelData);
            _stateSwitcher.SwitchTo<SpinWheelState>(typeof(CreateWheelState));
        }

        private void ReturnToChoiceState() => _stateSwitcher.SwitchTo<SelectWheelState>();
    }
}