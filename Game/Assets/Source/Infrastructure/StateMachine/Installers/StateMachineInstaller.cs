using Infrastructure.StateMachine.Factory;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.StateMachine.Installers
{
    public sealed class StateMachineInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<ApplicationStateMachine>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<StateFactory>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}