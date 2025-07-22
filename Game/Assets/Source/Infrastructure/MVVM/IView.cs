using UnityEngine;
using Infrastructure.Pool;

namespace Infrastructure.MVVM
{
    public interface IView : IPoolable
    {
        GameObject ViewObject { get; }
        void Subscribe();
        void Unsubscribe();
        void ForceUpdate();
        void Show();
        void Hide();
    }
}