using Cysharp.Threading.Tasks;

namespace Infrastructure.StateMachine.States
{
    public interface IPayloadState<in TPayload> : IExitableState
    {
        UniTask Enter(TPayload payload);
    }
}