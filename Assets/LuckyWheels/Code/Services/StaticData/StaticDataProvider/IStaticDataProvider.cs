using LuckyWheels.Code.Data.StaticData;
using LuckyWheels.Code.Data.StaticData.Sounds;
using LuckyWheels.Code.Data.StaticData.Wheel;

namespace LuckyWheels.Code.Services.StaticData.StaticDataProvider
{
    public interface IStaticDataProvider
    {
        PrefabsData LoadPrefabsData();
        SoundData LoadSoundData();
        LuckyWheelsSettingsConfig LoadLuckyWheelsConfig();
        WheelData[] LoadWheelsData();
    }
}