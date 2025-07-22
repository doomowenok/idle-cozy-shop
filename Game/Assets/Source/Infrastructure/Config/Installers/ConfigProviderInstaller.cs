using VContainer;
using VContainer.Unity;

namespace Infrastructure.Config.Installers
{
    public sealed class ConfigProviderInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<ResourcesConfigProvider>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}