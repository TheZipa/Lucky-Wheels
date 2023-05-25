using LuckyWheels.Code.Data.Progress;
using LuckyWheels.Code.Data.Statistics;
using LuckyWheels.Code.Extensions;
using LuckyWheels.Code.Services.StaticData;

namespace LuckyWheels.Code.Services.PersistentProgress
{
    public class PersistentPlayerProgress : IPersistentProgress
    {
        public PlayerProgress Progress { get; set; }
        public int CurrentWheelIndex { get; set; }
        private readonly int _maxStatisticCount;

        public PersistentPlayerProgress(IStaticData staticData) =>
            _maxStatisticCount = staticData.LuckyWheelsConfig.SpinStatisticCount;

        public void AddSpinHistory(int wheelIndex, string resultSector)
        {
            if (Progress.SpinHistory.Count == _maxStatisticCount)
                RefreshExistingSpinHistory(wheelIndex, resultSector);
            else
                AddNewSpinHistory(wheelIndex, resultSector);
        }

        private void AddNewSpinHistory(int wheelIndex, string resultSector) =>
            Progress.SpinHistory.Add(new SpinHistoryData()
            {
                WheelIndex = wheelIndex,
                ResultSector = resultSector
            });

        private void RefreshExistingSpinHistory(int wheelIndex, string resultSector)
        {
            SpinHistoryData spinHistoryData = Progress.SpinHistory.GetLastWithRemove();
            spinHistoryData.WheelIndex = wheelIndex;
            spinHistoryData.ResultSector = resultSector;
            Progress.SpinHistory.Insert(0, spinHistoryData);
        }
    }
}