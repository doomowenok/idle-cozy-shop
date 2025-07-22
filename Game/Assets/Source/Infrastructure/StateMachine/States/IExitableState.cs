using Cysharp.Threading.Tasks;

namespace Infrastructure.StateMachine.States
{
    public interface IExitableState
    {
        UniTask Exit();
    }
}