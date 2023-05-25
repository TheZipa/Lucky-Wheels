using LuckyWheels.Code.Data.StaticData;
using LuckyWheels.Code.Data.StaticData.Sounds;
using LuckyWheels.Code.Data.StaticData.Wheel;

namespace LuckyWheels.Code.Services.StaticData
{
    public interface IStaticData
    {
        PrefabsData Prefabs { get; }
        LuckyWheelsSettingsConfig LuckyWheelsConfig { get; }
        SoundData SoundData { get; }
        WheelData[] WheelsData { get; }
    }
}