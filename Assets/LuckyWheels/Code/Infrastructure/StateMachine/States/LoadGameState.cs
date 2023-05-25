using LuckyWheels.Code.Infrastructure.StateMachine.StateSwitcher;
using LuckyWheels.Code.Services.Factories.UIFactory;
using LuckyWheels.Code.Services.SceneLoader;
using UnityEngine;

namespace LuckyWheels.Code.Infrastructure.StateMachine.States
{
    public class LoadGameState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IUIFactory _uiFactory;
        private readonly ISceneLoader _sceneLoader;

        private const string GameScene = "Game";

        public LoadGameState(IStateSwitcher stateSwitcher, IUIFactory uiFactory, ISceneLoader sceneLoader)
        {
            _stateSwitcher = stateSwitcher;
            _uiFactory = uiFactory;
            _sceneLoader = sceneLoader;
        }

        public void Enter() => _sceneLoader.LoadScene(GameScene, () =>
        {
            CreateScreens();
            _stateSwitcher.SwitchTo<MenuState>();
        });

        public void Exit()
        {
        }

        private void CreateScreens()
        {
            Transform rootCanvas = _uiFactory.CreateRootCanvas().transform;
            _uiFactory.CreateMainMenuScreen(rootCanvas);
            _uiFactory.CreateWheelScreenView(rootCanvas);
            _uiFactory.CreateSelectWheelScreenView(rootCanvas);
            _uiFactory.CreateSpinWheelScreenView(rootCanvas);
            _uiFactory.CreateStatisticScreen(rootCanvas);
            _uiFactory.CreateAnimatedHead(rootCanvas);
            _uiFactory.CreateWheel(rootCanvas);
        }
    }
}