using VContainer;
using VContainer.Unity;

namespace Infrastructure.Localization.Installers
{
    public sealed class LocalizationInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<LocalizationService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}