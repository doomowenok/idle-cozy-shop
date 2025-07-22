using Infrastructure.StateMachine.States;
using Cysharp.Threading.Tasks;

namespace Infrastructure.StateMachine
{
    public interface IApplicationStateMachine
    {
        void AddState<TState>(TState instance) where TState : class, IExitableState;
        UniTask Enter<TState>() where TState : class, IState;
        UniTask Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>;
    }
}