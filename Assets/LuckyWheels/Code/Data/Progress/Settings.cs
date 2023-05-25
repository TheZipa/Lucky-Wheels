using System;

namespace LuckyWheels.Code.Data.Progress
{
    [Serializable]
    public class Settings
    {
        public bool IsEffectsSoundActive;
        public float EffectsVolume;

        public Settings()
        {
            IsEffectsSoundActive = true;
            EffectsVolume = 0.55f;
        }
    }
}