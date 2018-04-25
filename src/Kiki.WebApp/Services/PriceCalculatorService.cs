namespace Kiki.WebApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Models;

    public class PriceCalculatorService : IPriceCalculatorService
    {
        public int CalculateFinalPrice(IEnumerable<DiscountRule> rules, int size, decimal price, decimal discount)
        {
            var finalPrice = decimal.Ceiling(price - (price / 100 * discount));
            var margin = rules.FirstOrDefault(r => r.Size == size && finalPrice >= r.FromPrice && finalPrice <= r.ToPrice)?.Margin;
            return margin != null ? Convert.ToInt32(finalPrice + margin.Value) : 0;
        }
    }
}