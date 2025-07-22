using VContainer;
using VContainer.Unity;

namespace Infrastructure.EntityWorldManager.Installers
{
    public sealed class EntityWorldManagerInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<WorldManager>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}