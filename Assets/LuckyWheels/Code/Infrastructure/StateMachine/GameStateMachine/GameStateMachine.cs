using System;
using System.Collections.Generic;
using LuckyWheels.Code.Infrastructure.StateMachine.States;
using LuckyWheels.Code.Infrastructure.StateMachine.StateSwitcher;
using LuckyWheels.Code.Services.StateFactory;

namespace LuckyWheels.Code.Infrastructure.StateMachine.GameStateMachine
{
    public class GameStateMachine : IGameStateMachine, IDisposable
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private readonly IStateSwitcher _stateSwitcher;

        private IExitableState _activeState;

        public GameStateMachine(IStateFactory stateFactory, IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(LoadPersistentEntityState)] = stateFactory.Create<LoadPersistentEntityState>(),
                [typeof(LoadProgressState)] = stateFactory.Create<LoadProgressState>(),
                [typeof(LoadGameState)] = stateFactory.Create<LoadGameState>(),
                [typeof(MenuState)] = stateFactory.Create<MenuState>(),
                [typeof(CreateWheelState)] = stateFactory.Create<CreateWheelState>(),
                [typeof(SelectWheelState)] = stateFactory.Create<SelectWheelState>(),
                [typeof(SpinWheelState)] = stateFactory.Create<SpinWheelState>(),
                [typeof(StatisticState)] = stateFactory.Create<StatisticState>()
            };
            _stateSwitcher.OnStateSwitched += Enter;
            _stateSwitcher.OnStateSwitchedPayloaded += EnterPayload;
        }

        public void Dispose()
        {
            _stateSwitcher.OnStateSwitched -= Enter;
            _stateSwitcher.OnStateSwitchedPayloaded -= EnterPayload;
        }

        private void Enter(Type enterState)
        {
            IExitableState activeState = ChangeState(enterState);
            if(activeState is IState state) state.Enter();
        }

        private void EnterPayload(Type enterState, object payload)
        {
            IExitableState activeState = ChangeState(enterState);
            if(activeState is IPayloadedState state) state.Enter(payload);
        }

        private IExitableState ChangeState(Type enterState)
        {
            _activeState?.Exit();
            IExitableState exitableState = _states[enterState];
            _activeState = exitableState;
            return exitableState;
        }

        ~GameStateMachine() => _activeState.Exit();
    }
}