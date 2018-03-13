using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kiki.WebApp.Data.Models;

namespace Kiki.WebApp.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public IndexModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                Product = new List<Product>();
                return;
            }
            Product = await _context.Products
                .Where(p => EF.Functions.Like(p.Catalog.Name, ToLikeFormat(searchString))
                            || EF.Functions.Like(p.Name, ToLikeFormat(searchString))
                            || EF.Functions.Like(p.Info1, ToLikeFormat(searchString))
                            || EF.Functions.Like(p.Info2, ToLikeFormat(searchString))
                            || EF.Functions.Like(p.Info3, ToLikeFormat(searchString))
                            || EF.Functions.Like(p.Reference, ToLikeFormat(searchString))
                )
                .Select(p => new Product
                {
                    Id = p.Id,
                    Info1 = p.Info1,
                    Info2 = p.Info2,
                    Info3 = p.Info3,
                    Info4 = p.Info4,
                    Info5 = p.Catalog.Name,
                    Name = p.Name,
                    BasePrice = p.BasePrice,
                    FinalPrice = p.FinalPrice,
                    Dimension = p.Dimension,
                    EAN = p.EAN,
                    Reference = p.Reference,
                    Size = p.Size
                })
                .Take(100)
                .ToListAsync();
        }

        public static string ToLikeFormat(string s) => $"%{s}%";
    }
}
