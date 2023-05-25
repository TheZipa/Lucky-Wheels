using LuckyWheels.Code.Data.StaticData;
using LuckyWheels.Code.Data.StaticData.Sounds;
using LuckyWheels.Code.Data.StaticData.Wheel;
using LuckyWheels.Code.Services.StaticData.StaticDataProvider;

namespace LuckyWheels.Code.Services.StaticData
{
    public class StaticData : IStaticData
    {
        public PrefabsData Prefabs { get; private set; }
        public LuckyWheelsSettingsConfig LuckyWheelsConfig { get; private set; }
        public SoundData SoundData { get; private set; }
        public WheelData[] WheelsData { get; private set; }
        
        private readonly IStaticDataProvider _staticDataProvider;

        public StaticData(IStaticDataProvider staticDataProvider)
        {
            _staticDataProvider = staticDataProvider;
            LoadStaticData();
        }

        private void LoadStaticData()
        {
            Prefabs = _staticDataProvider.LoadPrefabsData();
            LuckyWheelsConfig = _staticDataProvider.LoadLuckyWheelsConfig();
            SoundData = _staticDataProvider.LoadSoundData();
            WheelsData = _staticDataProvider.LoadWheelsData();
        }
    }
}