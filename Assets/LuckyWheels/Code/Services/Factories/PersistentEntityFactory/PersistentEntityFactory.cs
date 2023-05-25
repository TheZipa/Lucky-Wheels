using LuckyWheels.Code.Core.Popup;
using LuckyWheels.Code.Core.Settings;
using LuckyWheels.Code.Core.TopPanel;
using LuckyWheels.Code.Services.EntityContainer;
using LuckyWheels.Code.Services.PersistentProgress;
using LuckyWheels.Code.Services.SaveLoad;
using LuckyWheels.Code.Services.Sound;
using LuckyWheels.Code.Services.StaticData;
using UnityEngine;

namespace LuckyWheels.Code.Services.Factories.PersistentEntityFactory
{
    public class PersistentEntityFactory : IPersistentEntityFactory
    {
        private readonly IEntityContainer _entityContainer;
        private readonly IStaticData _staticData;
        private readonly IPersistentProgress _persistentProgress;
        private readonly ISaveLoad _saveLoad;
        private readonly ISoundService _soundService;

        public PersistentEntityFactory(IEntityContainer entityContainer, IStaticData staticData,
            IPersistentProgress persistentProgress, ISaveLoad saveLoad, ISoundService soundService)
        {
            _entityContainer = entityContainer;
            _staticData = staticData;
            _persistentProgress = persistentProgress;
            _saveLoad = saveLoad;
            _soundService = soundService;
        }

        public TopPanelView CreateTopPanelView(Transform parent)
        {
            TopPanelView topPanelView = Object.Instantiate(_staticData.Prefabs.TopPanelViewPrefab, parent);
            topPanelView.Construct(_entityContainer.GetEntity<SettingsView>(), _soundService);
            _entityContainer.RegisterEntity(topPanelView);
            return topPanelView;
        }

        public SettingsView CreateSettingsPanel(Transform parent)
        {
            SettingsView settingsView = Object.Instantiate(_staticData.Prefabs.SettingsViewPrefab, parent);
            settingsView.Construct(_persistentProgress.Progress.Settings, _soundService);
            _entityContainer.RegisterEntity(new SettingsPanel(settingsView, 
                _persistentProgress, _saveLoad, _soundService));
            _entityContainer.RegisterEntity(settingsView);
            return settingsView;
        }

        public Popup CreatePopup(Transform parent)
        {
            Popup popup = Object.Instantiate(_staticData.Prefabs.PopupPrefab, parent);
            popup.Construct(_soundService);
            _entityContainer.RegisterEntity(popup);
            return popup;
        }
    }
}