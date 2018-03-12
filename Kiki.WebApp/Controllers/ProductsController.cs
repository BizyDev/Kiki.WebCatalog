namespace Kiki.WebApp.Controllers
{
    using System;
    using System.Collections.Immutable;
    using System.IO;
    using System.Linq;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using OfficeOpenXml;

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Calculate()
        {
            var catalogs = _context.Catalogs.ToImmutableList();
            var rules = _context.DiscountRules.ToImmutableList();
            foreach (var catalog in catalogs)
            {
                
            }

            return Ok();
        }


    }
}