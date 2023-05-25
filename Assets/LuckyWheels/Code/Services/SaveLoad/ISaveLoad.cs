using LuckyWheels.Code.Data.Progress;

namespace LuckyWheels.Code.Services.SaveLoad
{
    public interface ISaveLoad
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}