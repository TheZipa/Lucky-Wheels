using LuckyWheels.Code.Data.Progress;

namespace LuckyWheels.Code.Services.PersistentProgress
{
    public interface IPersistentProgress
    {
        PlayerProgress Progress { get; set; }
        int CurrentWheelIndex { get; set; }
        void AddSpinHistory(int wheelIndex, string resultSector);
    }
}