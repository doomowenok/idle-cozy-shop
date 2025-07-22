using VContainer;
using VContainer.Unity;

namespace Gameplay.Services.Physics
{
    public sealed class PhysicsInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<ObjectCollisionController>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}