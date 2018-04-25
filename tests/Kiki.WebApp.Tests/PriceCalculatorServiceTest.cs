namespace Kiki.WebApp.Tests
{
    using Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services;

    public class PriceCalculatorServiceTest
    {
        private readonly PriceCalculatorService _priceCalculatorService;
        public PriceCalculatorServiceTest()
        {
            _priceCalculatorService = new PriceCalculatorService();
        }

        [TestMethod]
        [DataRow(16, 202, 52, 157)]
        [DataRow(13, 92, 66, 64)]
        public void ShouldCalculateFinalPrice(int size, int price, int discount, int result)
        {
            var finalPrice = _priceCalculatorService.CalculateFinalPrice(ApplicationDbContextSeed.Rules, size, price, discount);
            Assert.AreEqual(finalPrice, result);
        }
    }
}