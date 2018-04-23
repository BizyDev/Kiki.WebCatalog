using Kiki.WebApp.Data;
using Kiki.WebApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Kiki.WebApp.Pages.Catalogs
{
    using Microsoft.AspNetCore.Http;
    using Services;
    using System.Collections.Immutable;
    using System.IO;

    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ExcelReaderService _excelReaderService;

        [BindProperty]
        public IFormFile CatalogFile { get; set; }

        [BindProperty]
        public bool SyncProducts { get; set; }

        public CreateModel(ApplicationDbContext context, ExcelReaderService excelReaderService)
        {
            _context = context;
            _excelReaderService = excelReaderService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Catalog Catalog { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (CatalogFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    CatalogFile.CopyTo(ms);
                    Catalog.File = ms.ToArray();
                }
            }
            _context.Catalogs.Add(Catalog);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            if (!SyncProducts) return RedirectToPage("./Index");
            var rules = _context.DiscountRules.ToImmutableList();
            var products = _excelReaderService.GetLines(Catalog, rules);
            await _context.Products.AddRangeAsync(products).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return RedirectToPage("./Index");
        }
    }
}