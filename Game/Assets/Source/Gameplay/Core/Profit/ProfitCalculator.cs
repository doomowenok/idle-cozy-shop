using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.Time;

namespace Gameplay.Core
{
    public class ProfitCalculator
    {
        private readonly ITimeService _timeService;
        private readonly Profit _profit;
        private readonly List<Shop> _openedShops = new List<Shop>();

        private CancellationTokenSource _calculationProfitCts;
        
        public bool CalculationInProgress => !_calculationProfitCts.IsCancellationRequested;

        public ProfitCalculator(ITimeService timeService, Profit profit)
        {
            _timeService = timeService;
            _profit = profit;
        }
        
        public void AddShopToCalculation(Shop shop)
        {
            _openedShops.Add(shop);
        }
        
        public async UniTaskVoid StartCalculatingProfit()
        {
            _calculationProfitCts = new CancellationTokenSource();
            
            while (!_calculationProfitCts.IsCancellationRequested)
            {
                float deltaTime = _timeService.DeltaTime;
                
                foreach (Shop shop in _openedShops)
                {
                    shop.TimeToCollect.Value -= deltaTime;

                    if (shop.TimeToCollect.Value <= 0.0f)
                    {
                        _profit.Money.Value += shop.GetProfit();
                        shop.ResetProfitTime();
                    }
                }

                await UniTask.WaitForSeconds(deltaTime);
            }
        }

        public void StopCalculatingProfit()
        {
            _calculationProfitCts?.Cancel();
        }
    }
}