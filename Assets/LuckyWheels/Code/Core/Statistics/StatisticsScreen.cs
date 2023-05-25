using LuckyWheels.Code.Data.Statistics;
using UnityEngine;
using Screen = LuckyWheels.Code.Core.ScreenViews.Screen;

namespace LuckyWheels.Code.Core.Statistics
{
    public class StatisticsScreen : Screen
    {
        public Transform Content;

        [SerializeField] private GameObject _noHistoryHint;
        private SpinStatisticView[] _spinStatisticViews;

        public void Construct(SpinStatisticView[] spinStatisticViews) => _spinStatisticViews = spinStatisticViews;

        public void ShowStatistic(HistoryViewData[] viewsData)
        {
            if (viewsData.Length == 0)
            {
                _noHistoryHint.SetActive(true);
                return;
            }

            _noHistoryHint.SetActive(false);
            ConfigureSpinStatisticViews(viewsData);
        }

        private void ConfigureSpinStatisticViews(HistoryViewData[] viewsData)
        {
            for (int i = 0; i < viewsData.Length; i++)
            {
                SpinStatisticView spinStatisticView = _spinStatisticViews[i];
                spinStatisticView.Configure(viewsData[i]);
                spinStatisticView.Show();
            }
        }
    }
}