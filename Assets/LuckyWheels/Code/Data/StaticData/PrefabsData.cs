using LuckyWheels.Code.Core.Popup;
using LuckyWheels.Code.Core.ScreenViews;
using LuckyWheels.Code.Core.Settings;
using LuckyWheels.Code.Core.Statistics;
using LuckyWheels.Code.Core.TopPanel;
using LuckyWheels.Code.Core.Wheel;
using UnityEngine;

namespace LuckyWheels.Code.Data.StaticData
{
    [CreateAssetMenu(fileName = "Prefabs Data", menuName = "Static Data/Prefabs Data")]
    public class PrefabsData : ScriptableObject
    {
        public GameObject RootCanvasPrefab;
        [Header("Persistent")]
        public SettingsView SettingsViewPrefab;
        public TopPanelView TopPanelViewPrefab;
        public Popup PopupPrefab;
        public AnimatedHead AnimatedHeadPrefab;
        [Header("Screens")] 
        public MainMenuScreenView MainMenuScreenViewPrefab;
        public CreateWheelScreenView CreateWheelScreenViewPrefab;
        public SelectWheelScreenView SelectWheelScreenViewPrefab;
        public SpinWheelScreenView SpinWheelScreenViewPrefab;
        [Header("Wheel")] 
        public WheelView WheelViewPrefab;
        public WheelSectorView WheelSectorViewPrefab;
        [Header("Statistic")] 
        public StatisticsScreen StatisticsScreenPrefab;
        public SpinStatisticView SpinStatisticViewPrefab;
    }
}