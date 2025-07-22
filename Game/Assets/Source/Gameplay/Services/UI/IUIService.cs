using Cysharp.Threading.Tasks;

namespace Gameplay.Services.UI
{
    public interface IUIService
    {
        UniTask Show(UIViewType viewType);
        void Hide(UIViewType viewType);
    }
}