using System.Collections.Generic;
using System.Linq;
using LuckyWheels.Code.Data.Enums;
using LuckyWheels.Code.Data.Progress;
using LuckyWheels.Code.Data.StaticData.Sounds;
using UnityEngine;

namespace LuckyWheels.Code.Services.Sound
{
    public class SoundService : MonoBehaviour, ISoundService
    {
        public bool EffectsMuted
        {
            get => _effectsSource.mute;
            set => _effectsSource.mute = !value;
        }
        
        [SerializeField] private AudioSource _effectsSource;
        
        private Dictionary<SoundId, AudioClipData> _sounds;
        
        public void Construct(SoundData soundData, Settings userSettings)
        {
            _sounds = soundData.AudioEffectClips.ToDictionary(s => s.Id);
            _effectsSource.volume = userSettings.EffectsVolume;
            _effectsSource.mute = !userSettings.IsEffectsSoundActive;
        }
        
        public void PlayEffectSound(SoundId soundId) =>
            _effectsSource.PlayOneShot(_sounds[soundId].Clip);
        
        public void SetEffectsVolume(float volume) =>
            _effectsSource.volume = volume;
    }
}