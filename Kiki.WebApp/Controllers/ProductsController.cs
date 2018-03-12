namespace Kiki.WebApp.Controllers
{
    using System.Collections.Immutable;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ExcelReaderService _excelReader;

        public ProductsController(ApplicationDbContext context, ExcelReaderService excelReader)
        {
            _context = context;
            _excelReader = excelReader;
        }

        [HttpGet]
        public IActionResult Calculate()
        {
            var catalogs = _context.Catalogs.ToImmutableList();
            var rules = _context.DiscountRules.ToImmutableList();
            foreach (var catalog in catalogs)
            {
                if (!System.IO.File.Exists(@"C:\workspace\#lab\Kiki.WebCatalog\Docs\" + catalog.FilePath)) continue;
                var products = _excelReader.GetLines(catalog, rules,  @"C:\workspace\#lab\Kiki.WebCatalog\Docs");
                _context.Products.AddRangeAsync(products);
                _context.SaveChangesAsync();
            }

            return Ok();
        }
    }
}