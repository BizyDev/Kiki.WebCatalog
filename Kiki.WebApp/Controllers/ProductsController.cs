using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kiki.WebApp.Controllers
{
    using System.Collections.Immutable;
    using System.IO;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Internal;
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
                var existingFile = new FileInfo(catalog.FilePath);
                using (var package = new ExcelPackage(existingFile))
                {
                    // get the first worksheet in the workbook
                    var worksheet = package.Workbook.Worksheets[catalog.SheetIndex];

                    var start = worksheet.Dimension.Start;
                    var end = worksheet.Dimension.End;
                    for (int row = start.Row; row <= end.Row; row++)
                    {
                        var s = Product.StringToSize(worksheet.Cells[catalog.SizeColumn + row].GetValue<string>(), catalog.SizeFormat);
                        _context.Products.Add(new Product
                        {
                            BasePrice = worksheet.Cells[catalog.PriceColumn + row].GetValue<decimal>(),
                            Size = s,
                            Reference = worksheet.Cells[catalog.ReferenceColumn + row].GetValue<string>(),
                            Info1 = worksheet.Cells[catalog.Info1Column + row].GetValue<string>(),
                            Info2 = worksheet.Cells[catalog.Info2Column + row].GetValue<string>(),
                            Info3 = worksheet.Cells[catalog.Info3Column + row].GetValue<string>(),
                            CatalogId = catalog.Id,
                            FinalPrice = CalculateFinalPrice(rules, s, worksheet.Cells[catalog.PriceColumn + row].GetValue<decimal>(), catalog.DiscountPercentage)
                        });
                    }
                }
            }
        }

        public int CalculateFinalPrice(ImmutableList<DiscountRule> rules, int size, decimal price, decimal discount)
        {
            var finalPrice = price - (price / 100 * discount);
            var margin = rules.FirstOrDefault(r => r.Size == size && r.FromPrice >= finalPrice && r.ToPrice <= finalPrice)?
                            .Margin;
            return margin != null ? Convert.ToInt32(finalPrice + margin.Value) : 0;
        }
    }
}