using System;
using Infrastructure.Time;
using UniRx;
using UnityEngine;

namespace Gameplay.Core.Test
{
    public class TestProfit : MonoBehaviour
    {
        private void Start()
        {
            Shop shop = new Shop();
            Profit profit = new Profit();
            ProfitCalculator calculator = new ProfitCalculator(new TimeService(), profit);

            calculator.AddShopToCalculation(shop);
            calculator.StartCalculatingProfit();

            Observable
                .EveryUpdate()
                .Throttle(TimeSpan.FromSeconds(1))
                .Subscribe(_ => Debug.Log(profit.Money.Value))
                .AddTo(this);
        }
    }
}