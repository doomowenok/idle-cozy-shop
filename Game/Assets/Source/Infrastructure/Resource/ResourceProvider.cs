using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;

namespace Infrastructure.Resource
{
    public sealed class ResourceProvider : IResourceProvider
    {
        private readonly IDictionary<Type, MonoBehaviour> _loadedAssets = new Dictionary<Type, MonoBehaviour>(64);
        
        public async UniTask<TObject> Get<TObject>(string name) where TObject : MonoBehaviour
        {
            if (_loadedAssets.TryGetValue(typeof(TObject), out MonoBehaviour asset))
            {
                return asset as TObject;
            }
            
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(name);
            
            while (!handle.IsDone)
            {
                await UniTask.Yield();
            }

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"Failed to load asset: {name}");
                return null;
            }

            TObject result = handle.Result.GetComponent<TObject>();

            if (result == null)
            {
                Debug.LogError($"Possible missing component of type {typeof(TObject).Name} in asset {name}.");
                return null;
            }
            
            _loadedAssets.Add(typeof(TObject), result);
            
            return result;
        }

        public void Release<TObject>(TObject instance) where TObject : MonoBehaviour
        {
            Addressables.Release(instance);
            _loadedAssets.Remove(typeof(TObject));
        }

        public async UniTask<SpriteAtlas> GetSpriteAtlas(string name)
        {
            AsyncOperationHandle<SpriteAtlas> handle = Addressables.LoadAssetAsync<SpriteAtlas>(name);
            
            while (!handle.IsDone)
            {
                await UniTask.Yield();
            }

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"Failed to load asset: {name}");
                return null;
            }

            SpriteAtlas result = handle.Result;

            if (result == null)
            {
                Debug.LogError($"Possible missing component of type SpriteAtlas in asset {name}.");
                return null;
            }
            
            return result;
        }

        public void ReleaseSpriteAtlas(SpriteAtlas atlas)
        {
            Addressables.Release(atlas);
        }
    }
}