using System.Collections.Generic;
using System.Linq;
using LuckyWheels.Code.Core.Statistics;
using LuckyWheels.Code.Core.TopPanel;
using LuckyWheels.Code.Data.StaticData.Wheel;
using LuckyWheels.Code.Data.Statistics;
using LuckyWheels.Code.Infrastructure.StateMachine.StateSwitcher;
using LuckyWheels.Code.Services.EntityContainer;
using LuckyWheels.Code.Services.PersistentProgress;
using LuckyWheels.Code.Services.StaticData;

namespace LuckyWheels.Code.Infrastructure.StateMachine.States
{
    public class StatisticState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IEntityContainer _entityContainer;
        private readonly IPersistentProgress _persistentProgress;
        private readonly HistoryViewData[] _historyViewsData;
        private readonly WheelData[] _wheelsData;

        private TopPanelView _topPanelView;
        private StatisticsScreen _statisticsScreen;

        public StatisticState(IStateSwitcher stateSwitcher, IEntityContainer entityContainer, 
            IPersistentProgress persistentProgress, IStaticData staticData)
        {
            _stateSwitcher = stateSwitcher;
            _entityContainer = entityContainer;
            _persistentProgress = persistentProgress;
            _wheelsData = staticData.WheelsData;
            _historyViewsData = new HistoryViewData[staticData.LuckyWheelsConfig.SpinStatisticCount];
            for (int i = 0; i < _historyViewsData.Length; i++)
                _historyViewsData[i] = new HistoryViewData();
        }
        
        public void Enter()
        {
            InitializeTopPanel();
            CollectUserSpinHistory();
            InitializeStatisticScreen();
        }
        
        public void Exit()
        {
            _topPanelView.HideTitle();
            _topPanelView.HideBackButton();
            _topPanelView.OnBackClick -= ReturnToMainMenu;
            _statisticsScreen.Hide();
        }

        private void InitializeTopPanel()
        {
            _topPanelView = _entityContainer.GetEntity<TopPanelView>();
            _topPanelView.ShowTitle("Statistics");
            _topPanelView.ShowBackButton();
            _topPanelView.OnBackClick += ReturnToMainMenu;
        }

        private void InitializeStatisticScreen()
        {
            _statisticsScreen = _entityContainer.GetEntity<StatisticsScreen>();
            _statisticsScreen.ShowStatistic(_historyViewsData.Take(_persistentProgress.Progress.SpinHistory.Count).ToArray());
            _statisticsScreen.Show();
        }

        private void CollectUserSpinHistory()
        {
            List<SpinHistoryData> spinHistoryData = _persistentProgress.Progress.SpinHistory;
            for (int i = 0; i < spinHistoryData.Count; i++)
                FillHistoryViewData(_historyViewsData[i], spinHistoryData[i]);
        }

        private void FillHistoryViewData(HistoryViewData historyViewData, SpinHistoryData spinHistoryData)
        {
            WheelData wheelData = _wheelsData[spinHistoryData.WheelIndex];
            historyViewData.HeadIcon = wheelData.Head;
            historyViewData.Question = wheelData.Question;
            historyViewData.SectorResult = spinHistoryData.ResultSector;
        }

        private void ReturnToMainMenu() => _stateSwitcher.SwitchTo<MenuState>();
    }
}