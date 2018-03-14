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
            optionsBuilder.UseSqlite(@"Data Source=C:\workspace\#lab\Kiki.WebCatalog\src\Kiki.WebApp\Kiki.sqlite");

            _excelReaderService = new ExcelReaderService(new Logger<ExcelReaderService>(new LoggerFactory()));
            _context = new ApplicationDbContext(optionsBuilder.Options);
            //_context.DiscountRules.AddRange(ApplicationDbContextSeed.Rules);
            //_context.SaveChanges();
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