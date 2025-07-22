using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.U2D;

namespace Infrastructure.Resource
{
    public interface IResourceProvider
    {
        UniTask<TObject> Get<TObject>(string name) where TObject : MonoBehaviour;
        void Release<TObject>(TObject instance) where TObject : MonoBehaviour;
        UniTask<SpriteAtlas> GetSpriteAtlas(string name);
        void ReleaseSpriteAtlas(SpriteAtlas atlas);
    }
}