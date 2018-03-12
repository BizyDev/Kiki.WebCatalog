namespace Kiki.WebCatalog.Tests
{
    using System.Collections.Immutable;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using WebApp.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WebApp.Data;
    using WebApp.Data.Models;

    [TestClass]
    public class ExcelReaderServiceTest
    {
        private readonly ExcelReaderService _excelReaderService;
        private readonly ApplicationDbContext _context;
        public ExcelReaderServiceTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("Test");
            
            _excelReaderService = new ExcelReaderService();
            _context = new ApplicationDbContext(optionsBuilder.Options);
            _context.DiscountRules.AddRange(ApplicationDbContextSeed.Rules);
            _context.SaveChanges();
        }

        [TestMethod]
        public void ShouldReadData_WhenFileExists()
        {
            var catalog = new Catalog
            {
                Name = "Goodyear, Dunlop, Fulda, Sava",
                SheetIndex = 0,
                PriceColumn = "K",
                SizeColumn = "C",
                ReferenceColumn = "A",
                Info1Column = "G",
                Info2Column = "D",
                Info3Column = "",
                StartLineNumber = 2,
                DiscountPercentage = 46,
                SizeFormat = (SizeFormatEnum)2,
                FilePath = "Prix été Goodyear, Dunlop, Sava, Fulda.xlsx"
            };

            var rules = _context.DiscountRules.ToImmutableList();

            var products = _excelReaderService.GetLines(catalog, rules, @"C:\workspace\#lab\Kiki.WebCatalog\Docs").ToList();

            Assert.IsTrue(products.Any());
        }
    }
}
