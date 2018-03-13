using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kiki.WebApp.Data.Models;

namespace Kiki.WebApp.Pages.Catalogs
{
    using System.Linq;

    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public IndexModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Catalog> Catalog { get;set; }

        public async Task OnGetAsync()
        {
            Catalog = await _context.Catalogs.Select(c => new Catalog
            {
                DiscountPercentage = c.DiscountPercentage,
                Id = c.Id,
                Name = c.Name,                
                Info1Column = c.Info1Column,
                Info2Column = c.Info2Column,
                Info3Column = c.Info3Column,
                Info4Column = c.Info4Column,
                Info5Column = c.Info5Column,
                PriceColumn = c.PriceColumn,
                Products = c.Products,
                ReferenceColumn = c.ReferenceColumn,
                SizeColumn = c.SizeColumn,
                SheetIndex = c.SheetIndex,
                SizeFormat = c.SizeFormat,
                StartLineNumber = c.StartLineNumber,
                DimensionColumn = c.DimensionColumn,
                EanColumn = c.EanColumn

            }).ToListAsync();
        }
    }
}
