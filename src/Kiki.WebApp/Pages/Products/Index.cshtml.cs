using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kiki.WebApp.Data.Models;

namespace Kiki.WebApp.Pages.Products
{
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        public IList<SelectListItem> Catalogs;
        public string SelectedCatalog { get; set; }
        public IndexModel(Data.ApplicationDbContext context)
        {
            _context = context;
            Catalogs = _context.Catalogs.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString()}).ToList();
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync(string selectedCatalog, string searchString)
        {
            int selectedId = 0;
            if (string.IsNullOrWhiteSpace(searchString) && !int.TryParse(selectedCatalog, out selectedId))
            {
                Product = new List<Product>();
                return;
            }
            
            Product = await _context.Products
                .Where(p => (EF.Functions.Like(p.Catalog.Name, ToLikeFormat(searchString))
                            || EF.Functions.Like(p.LoadIndexSpeedRating, ToLikeFormat(searchString))
                            || EF.Functions.Like(p.Profil, ToLikeFormat(searchString))
                            || EF.Functions.Like(p.Info1, ToLikeFormat(searchString))
                            || EF.Functions.Like(p.Info2, ToLikeFormat(searchString))
                            || EF.Functions.Like(p.Info3, ToLikeFormat(searchString))
                            || EF.Functions.Like(p.Dimension, ToLikeFormat(searchString))
                            || EF.Functions.Like(p.EAN, ToLikeFormat(searchString))
                            || EF.Functions.Like(p.Reference, ToLikeFormat(searchString))
                            || EF.Functions.Like(p.Brand, ToLikeFormat(searchString))) && (string.IsNullOrWhiteSpace(selectedCatalog) || p.CatalogId == selectedId)
                )
                .Select(p => new Product
                {
                    Id = p.Id,
                    Width = p.Width,
                    AspectRatio = p.AspectRatio,
                    LoadIndexSpeedRating = p.LoadIndexSpeedRating,
                    Profil = p.Profil,
                    Info1 = p.Info1,
                    Info2 = p.Info2,
                    Info3 = p.Info3,
                    BasePrice = p.BasePrice,
                    FinalPrice = p.FinalPrice,
                    Dimension = p.Dimension,
                    EAN = p.EAN,
                    Reference = p.Reference,
                    Dimater = p.Dimater,
                    Brand = p.Brand
                })
                .Take(500)
                .ToListAsync();
        }

        public static string ToLikeFormat(string s) => $"%{s}%";
    }
}