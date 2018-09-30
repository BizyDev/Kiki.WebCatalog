namespace Kiki.WebApp.Services
{
    using Data.Models;
    using System.Collections.Generic;

    public interface IPriceCalculatorService
    {
        int CalculateFinalPrice(IEnumerable<DiscountRule> rules, int size, decimal price, decimal discount);
        int CalculateFinalGaragePrice(IEnumerable<DiscountRule> rules, int size, decimal price, decimal discount);
    }
}