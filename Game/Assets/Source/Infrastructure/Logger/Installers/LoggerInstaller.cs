using VContainer;
using VContainer.Unity;

namespace Infrastructure.Logger.Installers
{
    public sealed class LoggerInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<ExternalLogger>(Lifetime.Singleton);
        }
    }
}