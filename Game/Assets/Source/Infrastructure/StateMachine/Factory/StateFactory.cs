using Infrastructure.StateMachine.States;
using VContainer;

namespace Infrastructure.StateMachine.Factory
{
    public sealed class StateFactory : IStateFactory
    {
        private readonly IObjectResolver _context;

        public StateFactory(IObjectResolver context)
        {
            _context = context;
        }
        
        public TState CreateState<TState>() where TState : class, IExitableState
        {
            return _context.Resolve<TState>();
        }
    }
}