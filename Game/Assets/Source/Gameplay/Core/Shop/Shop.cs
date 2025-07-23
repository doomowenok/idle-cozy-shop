using System.Collections.Generic;
using UniRx;
using ZLinq;

namespace Gameplay.Core
{
    public class Shop
    {
        public int Id { get; private set; }

        private readonly List<StorePart> _possibleParts;
        private ShopAppearance _appearance;

        public ReactiveProperty<float> TimeToCollect { get; private set; } = new ReactiveProperty<float>(0.0f);

        public void Initialize()
        {
            
        }

        public void UnlockStorePart()
        {
            
        }

        public void ChangeAppearance()
        {
            
        }

        public int GetProfit()
        {
            return _possibleParts
                .AsValueEnumerable()
                .Where(part => part.Opened)
                .Sum(part => part.Income);
        }

        public void ResetProfitTime()
        {
            TimeToCollect.Value = 10.0f;
        }
    }
}