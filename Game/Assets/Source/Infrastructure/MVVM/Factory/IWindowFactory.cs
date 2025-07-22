using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Infrastructure.MVVM.Factory
{
    public interface IWindowFactory
    {
        UniTask<TView> CreateWindow<TView>(string name, Transform parent = null) where TView : MonoBehaviour, IView;
        void DisposeWindow<TView>(TView instance) where TView : MonoBehaviour, IView;
    }
}