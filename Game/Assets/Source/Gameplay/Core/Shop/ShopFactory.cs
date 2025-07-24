using Cysharp.Threading.Tasks;
using Gameplay.Services.Physics;
using Infrastructure.Resource;
using UnityEngine;

namespace Gameplay.Core
{
    public sealed class ShopFactory
    {
        private readonly IResourceProvider _resourceProvider;
        private readonly IObjectCollisionController _objectCollisionController;

        public ShopFactory(
            IResourceProvider resourceProvider,
            IObjectCollisionController objectCollisionController)
        {
            _resourceProvider = resourceProvider;
            _objectCollisionController = objectCollisionController;
        }
        
        public async UniTask<ShopViewBase> CreateShop()
        {
            ShopViewBase shopViewPrefab = await _resourceProvider.Get<ShopViewBase>("");
            ShopViewBase shopView = Object.Instantiate(shopViewPrefab);
            _objectCollisionController.Register(shopView);
            return shopView;
        }
    }
}