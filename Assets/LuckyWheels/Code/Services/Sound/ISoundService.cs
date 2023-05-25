using LuckyWheels.Code.Data.Enums;
using LuckyWheels.Code.Data.Progress;
using LuckyWheels.Code.Data.StaticData.Sounds;

namespace LuckyWheels.Code.Services.Sound
{
    public interface ISoundService
    {
        bool EffectsMuted { get; set; }
        void Construct(SoundData soundData, Settings userSettings);
        void PlayEffectSound(SoundId soundId);
        void SetEffectsVolume(float volume);
    }
}