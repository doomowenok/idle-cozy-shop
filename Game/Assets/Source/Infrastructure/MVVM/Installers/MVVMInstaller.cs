using Infrastructure.MVVM.Factory;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.MVVM.Installers
{
    public sealed class MvvmInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<WindowFactory>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}