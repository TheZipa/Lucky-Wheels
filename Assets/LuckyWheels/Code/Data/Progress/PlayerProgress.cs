using System;
using System.Collections.Generic;
using LuckyWheels.Code.Data.Statistics;

namespace LuckyWheels.Code.Data.Progress
{
    [Serializable]
    public class PlayerProgress
    {
        public Settings Settings;
        public List<SpinHistoryData> SpinHistory;

        public PlayerProgress()
        {
            SpinHistory = new List<SpinHistoryData>(10);
            Settings = new Settings();
        }
    }
}