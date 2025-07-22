using VContainer;
using VContainer.Unity;

namespace Infrastructure.Time.Installers
{
    public sealed class TimeServiceInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<TimeService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}