using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Kiki.WebApp.Data;
using Kiki.WebApp.Data.Models;

namespace Kiki.WebApp.Pages.Catalogs
{
    using System.Collections.Immutable;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using Microsoft.AspNetCore.Http;
    using Services;

    public class CreateModel : PageModel
    {
        [BindProperty]
        [Display(Name = "Catalogue")]
        public IFormFile CatalogFile { get; set; }

        [BindProperty]
        public bool SyncProducts { get; set; }
        private readonly ApplicationDbContext _context;
        private readonly ExcelReaderService _excelReaderService;

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
            await _context.SaveChangesAsync();

            if (!SyncProducts) return RedirectToPage("./Index");
            var rules = _context.DiscountRules.ToImmutableList();
            var products = _excelReaderService.GetLines(Catalog, rules);
            await _context.Products.AddRangeAsync(products);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}