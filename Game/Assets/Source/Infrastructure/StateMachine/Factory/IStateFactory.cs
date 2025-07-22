using Infrastructure.StateMachine.States;

namespace Infrastructure.StateMachine.Factory
{
    public interface IStateFactory
    {
        TState CreateState<TState>() where TState : class, IExitableState;
    }
}