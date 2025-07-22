using Cysharp.Threading.Tasks;

namespace Infrastructure.StateMachine.States
{
    public interface IState : IExitableState
    {
        UniTask Enter();
    }
}