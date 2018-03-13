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
                Products = c.Products,
                ReferenceColumn = c.ReferenceColumn,
                SheetIndex = c.SheetIndex,
                SizeFormat = c.SizeFormat,
                StartLineNumber = c.StartLineNumber,
                DimensionColumn = c.DimensionColumn,
                AspectRatioColumn = c.AspectRatioColumn,
                BasePriceColumn = c.BasePriceColumn,
                BrandColumn = c.BrandColumn,
                DiameterColumn = c.BrandColumn,
                EANColumn = c.EANColumn,
                LoadIndexSpeedRatingColumn = c.LoadIndexSpeedRatingColumn,
                ProfilColumn = c.ProfilColumn,
                WidthColumn = c.WidthColumn
            }).ToListAsync();
        }
    }
}
