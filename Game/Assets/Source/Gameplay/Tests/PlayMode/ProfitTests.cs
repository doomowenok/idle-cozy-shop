using System.Collections;
using FluentAssertions;
using Gameplay.Core;
using Infrastructure.Time;
using UnityEngine;
using UnityEngine.TestTools;

namespace Gameplay.Tests.PlayMode
{
    public class ProfitTests
    {
        [UnityTest]
        public IEnumerator WhenTimePassed_ThenShopGenerateProfit()
        {
            // Arrange
            Shop shop = new Shop();
            shop.TimeToCollect.Value = 1.0f;
            
            Profit profit = new Profit();
            profit.Money.Value = 0;
            
            ProfitCalculator calculator = new ProfitCalculator(new TimeService(), profit);

            // Act
            calculator.AddShopToCalculation(shop);
            calculator.StartCalculatingProfit().Forget();
            yield return new WaitForSeconds(2.0f);
            calculator.StopCalculatingProfit();
            
            // Assert
            var exceptedProfit = shop.GetProfit();
            profit.Money.Value.Should().Be(exceptedProfit);
        }

        [UnityTest]
        public IEnumerator WhenCalculatingStart_ThenItCanBeSuccessfullyStopped()
        {
            // Arrange
            Profit profit = new Profit();
            ProfitCalculator calculator = new ProfitCalculator(new TimeService(), profit);
            
            // Act
            calculator.StartCalculatingProfit().Forget();
            yield return new WaitForSeconds(2.0f);
            calculator.StopCalculatingProfit();
            
            // Assert
            calculator.CalculationInProgress.Should().BeFalse();
        }
    }
}