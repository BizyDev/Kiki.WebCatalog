namespace Kiki.WebApp.Controllers.API
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
                var products = _excelReader.GetLines(catalog, rules);
                _context.Products.AddRangeAsync(products);
                _context.SaveChangesAsync();
            }

            return Ok();
        }
    }
}