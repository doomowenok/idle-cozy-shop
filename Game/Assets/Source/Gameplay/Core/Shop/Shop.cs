using System.Collections.Generic;
using UniRx;

namespace Gameplay.Core
{
    public class Shop
    {
        public int Id { get; private set; }

        private readonly List<StorePart> _parts;
        private ShopAppearance _appearance;

        public ReactiveProperty<float> TimeToCollect { get; private set; } = new ReactiveProperty<float>(0.0f);

        public void UnlockStorePart()
        {
            
        }

        public void ChangeAppearance()
        {
            
        }

        public int GetProfit()
        {
            return 1;
        }

        public void ResetProfitTime()
        {
            TimeToCollect.Value = 10.0f;
        }
    }
}