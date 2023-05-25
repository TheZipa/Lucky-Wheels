using LuckyWheels.Code.Infrastructure.StateMachine.States;

namespace LuckyWheels.Code.Services.StateFactory
{
    public interface IStateFactory
    {
        T Create<T>() where T : IExitableState;
    }
}