using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Gameplay.Core
{
    public sealed class LocalScope : LifetimeScope
    {
        [SerializeField] private ShopsProvider _shopsProvider;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_shopsProvider).As<ShopsProvider>();
        }
    }
}