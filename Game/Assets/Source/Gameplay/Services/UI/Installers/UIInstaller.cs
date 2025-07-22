using VContainer;
using VContainer.Unity;

namespace Gameplay.Services.UI
{
    public sealed class UIInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<UIService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}