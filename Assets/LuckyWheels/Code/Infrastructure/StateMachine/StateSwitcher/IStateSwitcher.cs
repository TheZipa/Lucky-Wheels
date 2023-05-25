using System;
using LuckyWheels.Code.Infrastructure.StateMachine.States;

namespace LuckyWheels.Code.Infrastructure.StateMachine.StateSwitcher
{
    public interface IStateSwitcher
    {
        event Action<Type> OnStateSwitched;
        event Action<Type, object> OnStateSwitchedPayloaded;
        void SwitchTo(Type state);
        void SwitchTo<TState>() where TState : class, IState;
        void SwitchTo<TState>(object payload) where TState : class, IPayloadedState;
    }
}