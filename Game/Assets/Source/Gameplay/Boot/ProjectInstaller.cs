using Gameplay.Services.Audio;
using VContainer;
using VContainer.Unity;
using Infrastructure.Pool.Installers;
using Infrastructure.Resource.Installers;
using Infrastructure.SceneLoading.Installers;
using Infrastructure.StateMachine.Installers;
using Infrastructure.MVVM.Installers;
using Infrastructure.Config.Installers;
using Gameplay.Services.Physics;
using Gameplay.Services.UI;
using Infrastructure.EntityWorldManager.Installers;
using Infrastructure.IdGenerator.Installers;
using Infrastructure.Localization.Installers;
using Infrastructure.SaveLoad;
using Infrastructure.Time.Installers;
using UnityEngine;

namespace Gameplay.Boot
{
    public sealed class ProjectInstaller : LifetimeScope
    {
        [SerializeField] private AudioService _audioService;
        
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }

        protected override void Configure(IContainerBuilder builder)
        {
            // MonoProviders
            builder.RegisterInstance<AudioService>(_audioService).AsImplementedInterfaces();
            
            // States
            builder.Register<BootState>(Lifetime.Singleton).AsSelf();

            // Infrastructure
            InstallBindings<StateMachineInstaller>(builder);
            InstallBindings<SceneLoaderInstaller>(builder);
            InstallBindings<ResourcesInstaller>(builder);
            InstallBindings<ObjectPoolInstaller>(builder);
            InstallBindings<MvvmInstaller>(builder);
            InstallBindings<ConfigProviderInstaller>(builder);
            InstallBindings<TimeServiceInstaller>(builder);
            InstallBindings<IdGeneratorInstaller>(builder);
            InstallBindings<EntityWorldManagerInstaller>(builder);

            // Services
            InstallBindings<PhysicsInstaller>(builder);
            InstallBindings<UIInstaller>(builder);
            InstallBindings<SaveLoadInstaller>(builder);
            InstallBindings<LocalizationInstaller>(builder);
            
            // Core
        }

        private static void InstallBindings<TInstaller>(IContainerBuilder builder) where TInstaller : IInstaller,  new() 
        {
            TInstaller installer = new TInstaller();
            installer.Install(builder);
        }
    }
}