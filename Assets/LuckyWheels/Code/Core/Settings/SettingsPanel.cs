using System;
using LuckyWheels.Code.Infrastructure.StateMachine.States;
using LuckyWheels.Code.Infrastructure.StateMachine.StateSwitcher;
using LuckyWheels.Code.Services.PersistentProgress;
using LuckyWheels.Code.Services.SaveLoad;
using LuckyWheels.Code.Services.Sound;
using UnityEditor;

namespace LuckyWheels.Code.Core.Settings
{
    public class SettingsPanel : IDisposable
    {
        private readonly SettingsView _view;
        private readonly IPersistentProgress _persistentProgress;
        private readonly ISaveLoad _saveLoad;
        private readonly ISoundService _soundService;

        public SettingsPanel(SettingsView view, IPersistentProgress persistentProgress,
            ISaveLoad saveLoad, ISoundService soundService)
        {
            _view = view;
            _persistentProgress = persistentProgress;
            _saveLoad = saveLoad;
            _soundService = soundService;

            SubscribeView();
        }
        
        public void Dispose()
        {
            _view.EffectSoundSettingsView.OnSwitch -= OnEffectSoundSwitch;
            _view.EffectSoundSettingsView.OnVolumeChanged -= OnEffectVolumeChanged;
        }

        private void SubscribeView()
        {
            _view.EffectSoundSettingsView.OnSwitch += OnEffectSoundSwitch;
            _view.EffectSoundSettingsView.OnVolumeChanged += OnEffectVolumeChanged;
        }

        private void OnEffectSoundSwitch(bool isActive)
        {
            _soundService.EffectsMuted = _persistentProgress.Progress.Settings.IsEffectsSoundActive = isActive;
            _saveLoad.SaveProgress();
        }

        private void OnEffectVolumeChanged(float volume)
        { 
            _persistentProgress.Progress.Settings.EffectsVolume = volume;
            _soundService.SetEffectsVolume(volume);
            _saveLoad.SaveProgress();
        }
    }
}