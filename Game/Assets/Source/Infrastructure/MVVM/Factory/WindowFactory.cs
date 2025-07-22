using UnityEngine;
using Infrastructure.Pool;
using Cysharp.Threading.Tasks;
using Infrastructure.Resource;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.MVVM.Factory
{
    public sealed class WindowFactory : IWindowFactory
    {
        private readonly IObjectResolver _context;
        private readonly IResourceProvider _resourceProvider;
        private readonly IObjectPool _objectPool;

        public WindowFactory(IObjectResolver context, IResourceProvider resourceProvider, IObjectPool objectPool)
        {
            _context = context;
            _resourceProvider = resourceProvider;
            _objectPool = objectPool;
        }
        
        public async UniTask<TView> CreateWindow<TView>(string name, Transform parent = null) where TView : MonoBehaviour, IView
        {
            if (!_objectPool.TryRent<TView>(out TView instance))
            {
                TView viewPrefab = await _resourceProvider.Get<TView>(name);
                instance = Object.Instantiate(viewPrefab);
            }

            instance.transform.SetParent(parent);
            _context.InjectGameObject(instance.gameObject);
            return instance;
        }

        public void DisposeWindow<TView>(TView instance) where TView : MonoBehaviour, IView
        {
            _objectPool.Release(instance);
        }
    }
}