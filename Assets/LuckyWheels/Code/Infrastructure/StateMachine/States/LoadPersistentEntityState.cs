using LuckyWheels.Code.Infrastructure.StateMachine.StateSwitcher;
using LuckyWheels.Code.Services.Factories.PersistentEntityFactory;
using LuckyWheels.Code.Services.Factories.UIFactory;
using UnityEngine;

namespace LuckyWheels.Code.Infrastructure.StateMachine.States
{
    public class LoadPersistentEntityState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IPersistentEntityFactory _persistentEntityFactory;
        private readonly IUIFactory _uiFactory;

        public LoadPersistentEntityState(IStateSwitcher stateSwitcher, IPersistentEntityFactory persistentEntityFactory,
            IUIFactory uiFactory)
        {
            _stateSwitcher = stateSwitcher;
            _persistentEntityFactory = persistentEntityFactory;
            _uiFactory = uiFactory;
        }
        
        public void Enter()
        {
            Transform rootCanvas = CreateRootCanvas().transform;
            CreatePersistentEntities(rootCanvas);
            _stateSwitcher.SwitchTo<LoadGameState>();
        }

        public void Exit()
        {
        }

        private GameObject CreateRootCanvas()
        {
            Canvas rootCanvas = _uiFactory.CreateRootCanvas().GetComponent<Canvas>();
            rootCanvas.sortingOrder = 10;
            Object.DontDestroyOnLoad(rootCanvas);
            return rootCanvas.gameObject;
        }

        private void CreatePersistentEntities(Transform rootCanvas)
        {
            _persistentEntityFactory.CreateSettingsPanel(rootCanvas);
            _persistentEntityFactory.CreateTopPanelView(rootCanvas).transform.SetAsFirstSibling();
            _persistentEntityFactory.CreatePopup(rootCanvas);
        }
    }
}