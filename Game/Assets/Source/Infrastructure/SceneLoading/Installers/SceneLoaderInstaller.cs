using VContainer;
using VContainer.Unity;

namespace Infrastructure.SceneLoading.Installers
{
    public sealed class SceneLoaderInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<SceneLoader>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}