namespace Kiki.WebApp.Pages.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        public IList<SelectListItem> Catalogs;

        [BindProperty]
        public bool IsKiki { get; set; }

        [BindProperty]
        public string Search { get; set; }

        [BindProperty]
        public string SelectedCatalog { get; set; }

        [BindProperty]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Que des chiffres please")]
        public int? Width { get; set; }

        [BindProperty]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Que des chiffres please")]
        public int? AspectRatio { get; set; }

        [BindProperty]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Que des chiffres please")]
        public int? Diameter { get; set; }

        public IndexModel(Data.ApplicationDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            IsKiki = User.IsInRole("Kiki") ? true : User.IsInRole("Admin");
            Catalogs = await _context.Catalogs.Where(c => IsKiki || c.DisplayForGarages).Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }).OrderBy(c => c.Text).ToListAsync();
        }

        public IList<Product> Product { get; set; } = new List<Product>();

        public async Task<IActionResult> OnPostAsync()
        {
            IsKiki = User.IsInRole("Kiki") ? true : User.IsInRole("Admin");
            Catalogs = await _context.Catalogs.Where(c => IsKiki || c.DisplayForGarages).Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }).OrderBy(c => c.Text).ToListAsync();
            if (!ModelState.IsValid || !int.TryParse(SelectedCatalog, out var selectedId) && !Width.HasValue && !AspectRatio.HasValue && !Diameter.HasValue && string.IsNullOrWhiteSpace(Search)) return Page();

            IsKiki = User.IsInRole("Kiki") ? true : User.IsInRole("Admin");
            
            Product = await _context.Products
                .Where(p =>
                    (!Width.HasValue || p.Width == Width)
                    && (!AspectRatio.HasValue || p.AspectRatio == AspectRatio)
                    && (!Diameter.HasValue || p.Dimater == Diameter)
                    && (selectedId < 1 || p.CatalogId == selectedId)
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
                    FinalPriceGarage = p.FinalPriceGarage,
                    Dimension = p.Dimension,
                    EAN = p.EAN,
                    Reference = p.Reference,
                    Dimater = p.Dimater,
                    Brand = p.Brand
                })
                .ToListAsync().ConfigureAwait(false);
            return Page();
        }
    }
}