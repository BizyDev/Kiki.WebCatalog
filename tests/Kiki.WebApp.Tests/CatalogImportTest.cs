namespace Kiki.WebApp.Tests
{
    using System.Collections.Immutable;
    using System.IO;
    using System.Linq;
    using Data;
    using Data.Models;
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

            _excelReaderService = new ExcelReaderService(new Logger<ExcelReaderService>(new LoggerFactory()));
            _context = new ApplicationDbContext(optionsBuilder.Options);
            _context.DiscountRules.AddRange(ApplicationDbContextSeed.Rules);
            _context.SaveChanges();
        }

        [TestMethod]
        public void ShouldAddProductsToDb_WhenFileExists()
        {
            var catalog = new Catalog
            {
                Name = "Goodyear, Dunlop, Fulda, Sava",
                SheetIndex = 0,
                BasePriceColumn = "K",
                DiameterColumn = "C",
                ReferenceColumn = "A",
                DimensionColumn = "G",
                Info2Column = "D",
                Info3Column = "",
                StartLineNumber = 2,
                DiscountPercentage = 46,
                SizeFormat = (SizeFormatEnum)2,
                File = File.ReadAllBytes(@"C:\Users\Roxtar\Desktop\kiki\Prix été Goodyear, Dunlop, Sava, Fulda.xlsx")
            };

            var rules = _context.DiscountRules.ToImmutableList();

            var products = _excelReaderService.GetLines(catalog, rules).ToList();

            Assert.IsTrue(products.Any());
        }
    }
}