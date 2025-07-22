using VContainer;
using VContainer.Unity;

namespace Infrastructure.Pool.Installers
{
    public sealed class ObjectPoolInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<ObjectPool>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}