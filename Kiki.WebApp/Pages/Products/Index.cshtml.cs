using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kiki.WebApp.Data.Models;

namespace Kiki.WebApp.Pages.Products
{
    using System.Linq;

    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public IndexModel(Kiki.WebApp.Data.ApplicationDbContext context)
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
                .Include(p => p.Catalog)
                .ToListAsync();
        }

        public static string ToLikeFormat(string s) => $"%{s}%";
    }
}
