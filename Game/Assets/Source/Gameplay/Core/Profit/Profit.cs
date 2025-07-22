using UniRx;

namespace Gameplay.Core
{
    public class Profit
    {
        public ReactiveProperty<int> Money { get; private set; } = new ReactiveProperty<int>(0);
    }
}