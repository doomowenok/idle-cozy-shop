using Cysharp.Threading.Tasks;
using Infrastructure.Localization;
using Infrastructure.SaveLoad;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;

namespace Gameplay.Boot
{
    public sealed class BootState : IState
    {
        private readonly IApplicationStateMachine _stateMachine;
        private readonly ISaveLoadService _saveLoadService;
        private readonly LocalizationService _localizationService;

        public BootState(
            IApplicationStateMachine stateMachine, 
            ISaveLoadService saveLoadService, 
            LocalizationService localizationService)
        {
            _stateMachine = stateMachine;
            _saveLoadService = saveLoadService;
            _localizationService = localizationService;
        }

        public UniTask Enter()
        {
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