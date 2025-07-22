using VContainer;
using VContainer.Unity;

namespace Infrastructure.IdGenerator.Installers
{
    public sealed class IdGeneratorInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<UniqueIdGenerator>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}