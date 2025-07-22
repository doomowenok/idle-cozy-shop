
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Resource.Installers
{
    public sealed class ResourcesInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<ResourceProvider>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}