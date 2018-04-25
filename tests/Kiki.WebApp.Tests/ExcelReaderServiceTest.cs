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
    public class ExcelReaderServiceTest
    {
        private readonly ExcelReaderService _excelReaderService;
        private readonly ApplicationDbContext _context;

        public ExcelReaderServiceTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("Test");

            _excelReaderService = new ExcelReaderService(new PriceCalculatorService(), new Logger<ExcelReaderService>(new LoggerFactory()));
            _context = new ApplicationDbContext(optionsBuilder.Options);
            _context.DiscountRules.AddRange(ApplicationDbContextSeed.Rules);
            _context.SaveChanges();
        }

        [TestMethod]
        public void ShouldReadData_WhenFileExists()
        {
            var catalog = new Catalog
            {
                Name = "Pirelli",
                SheetIndex = 0,
                BasePriceColumn = "D",
                DiameterColumn = "C",
                ReferenceColumn = "F",
                DimensionColumn = "C",
                Info2Column = "",
                Info3Column = "",
                StartLineNumber = 2,
                DiscountPercentage = 55,
                SizeFormat = (SizeFormatEnum)2,
                File = File.ReadAllBytes(@"C:\Users\Roxtar\Desktop\kiki\Prix été Pirelli.xlsx")
            };
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var rules = _context.DiscountRules.ToImmutableList();

            var products = _excelReaderService.GetLines(catalog, rules).ToList();
            products.ForEach(p => _context.Entry(p).State = EntityState.Added);
            _context.SaveChanges();
            Assert.IsTrue(_context.Products.Any());
        }
    }
}