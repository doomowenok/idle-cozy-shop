using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infrastructure.Config;
using Infrastructure.MVVM;
using Infrastructure.MVVM.Factory;
using UnityEngine;
using VContainer;

namespace Gameplay.Services.UI
{
    public sealed class UIService : IUIService
    {
        private readonly Dictionary<Type, IView> _activeViews = new Dictionary<Type, IView>();
        
        private readonly IWindowFactory _windowFactory;
        private readonly IConfigProvider _configProvider;
        private readonly IObjectResolver _context;

        public UIService(IWindowFactory windowFactory, IConfigProvider configProvider, IObjectResolver context)
        {
            _windowFactory = windowFactory;
            _configProvider = configProvider;
            _context = context;
        }

        public async UniTask Show(UIViewType viewType)
        {
            UIConfig config = _configProvider.GetConfig<UIConfig>("UI");

            switch (viewType)
            {
                case UIViewType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }

        public void Hide(UIViewType viewType)
        {
            switch (viewType)
            {
                case UIViewType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }

        private async UniTask Subscribe<TView, TViewModel>(string name) 
            where TView : MonoBehaviour, IView  
            where TViewModel : IViewModel
        {
            TView view = await _windowFactory.CreateWindow<TView>(name);
            TViewModel viewModel = _context.Resolve<TViewModel>();
            view.Subscribe();
            viewModel.Subscribe();
            view.ForceUpdate();
            view.Show();
            
            _activeViews.Add(typeof(TView), view);
        }
        
        private void Unsubscribe<TView, TViewModel>() 
            where TView : MonoBehaviour, IView  
            where TViewModel : IViewModel
        {
            TViewModel viewModel = _context.Resolve<TViewModel>();
            viewModel.Unsubscribe();

            TView view = _activeViews[typeof(TView)] as TView;
            
            if (view == null)
            {
                Debug.LogWarning($"View {typeof(TView).Name} not found");
                return;
            }

            _windowFactory.DisposeWindow<TView>(view);
            _activeViews.Remove(typeof(TView));
        }
    }
}
