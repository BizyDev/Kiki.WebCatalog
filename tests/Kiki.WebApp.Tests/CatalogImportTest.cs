namespace Kiki.WebApp.Tests
{
    using System.Collections.Immutable;
    using System.Linq;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services;

    [TestClass]
    public class CatalogImportTest
    {
        private readonly ExcelReaderService _excelReaderService;
        private readonly ApplicationDbContext _context;
        public CatalogImportTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("Test");

            _excelReaderService = new ExcelReaderService(new PriceCalculatorService(), new Logger<ExcelReaderService>(new LoggerFactory()));
            _context = new ApplicationDbContext(optionsBuilder.Options);
        }

        [TestMethod]
        public void ShouldAddProductsToDb_WhenFileExists()
        {
            var rules = _context.DiscountRules.ToImmutableList();

            foreach (var catalog in _context.Catalogs.ToList())
            {
                var products = _excelReaderService.GetLines(catalog, rules).ToList();
                _context.Products.AddRange(products);
                _context.SaveChanges();
            }
        }
    }
}