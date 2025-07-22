using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Time;
using UnityEngine;

namespace Gameplay.Core
{
    public class ProfitCalculator
    {
        private readonly ITimeService _timeService;
        private readonly Profit _profit;
        private readonly List<Shop> _openedShops;

        public ProfitCalculator(ITimeService timeService, Profit profit)
        {
            _timeService = timeService;
            _profit = profit;
        }
        
        public void AddShopToCalculation(Shop shop)
        {
            _openedShops.Add(shop);
        }
        
        public void StartCalculatingProfit()
        {
            _ = Task.Run(() =>
            {
                while (true)
                {
                    List<Shop> shops = UnityEngine.Pool.ListPool<Shop>.Get();
                    shops.AddRange(_openedShops);

                    foreach (Shop shop in shops)
                    {
                        shop.TimeToCollect.Value -= _timeService.DeltaTime;

                        if (shop.TimeToCollect.Value <= 0.0f)
                        {
                            _profit.Money.Value += shop.GetProfit();
                            Debug.Log($"Add profit.");
                            shop.ResetProfitTime();
                        }
                    }
                    
                    UnityEngine.Pool.ListPool<Shop>.Release(shops);
                }
            });
        }
    }
}