using LuckyWheels.Code.Data.StaticData;
using LuckyWheels.Code.Data.StaticData.Sounds;
using LuckyWheels.Code.Data.StaticData.Wheel;
using UnityEngine;

namespace LuckyWheels.Code.Services.StaticData.StaticDataProvider
{
    public class StaticDataProvider : IStaticDataProvider
    {
        private const string PrefabsDataPath = "StaticData/Prefabs Data";
        private const string SoundDataPath = "StaticData/Sound Data";
        private const string LuckyWheelsConfigPath = "StaticData/Lucky Wheels Config";
        private const string WheelsDataPath = "StaticData/Wheels/";

        public PrefabsData LoadPrefabsData() => Resources.Load<PrefabsData>(PrefabsDataPath);

        public SoundData LoadSoundData() => Resources.Load<SoundData>(SoundDataPath);

        public LuckyWheelsSettingsConfig LoadLuckyWheelsConfig() =>
            Resources.Load<LuckyWheelsSettingsConfig>(LuckyWheelsConfigPath);

        public WheelData[] LoadWheelsData() => Resources.LoadAll<WheelData>(WheelsDataPath);
    }
}