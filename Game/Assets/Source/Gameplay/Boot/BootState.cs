using Cysharp.Threading.Tasks;
using Infrastructure.Localization;
using Infrastructure.Logger;
using Infrastructure.SaveLoad;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;

namespace Gameplay.Boot
{
    public sealed class BootState : IState
    {
        private readonly IApplicationStateMachine _stateMachine;
        private readonly ISaveLoadService _saveLoadService;
        private readonly ILocalizationService _localizationService;
        private readonly ExternalLogger _logger;

        public BootState(
            IApplicationStateMachine stateMachine, 
            ISaveLoadService saveLoadService, 
            ILocalizationService localizationService,
            ExternalLogger logger)
        {
            _stateMachine = stateMachine;
            _saveLoadService = saveLoadService;
            _localizationService = localizationService;
            _logger = logger;
        }

        public UniTask Enter()
        {
            _logger.Initialize();
            
            _localizationService.Initialize();
            
            _saveLoadService.RegisterAutomatically();
            _saveLoadService.LoadSave();
            
            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}