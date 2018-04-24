using Kiki.WebApp.Data;
using Kiki.WebApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Kiki.WebApp.Pages.Catalogs
{
    using Microsoft.AspNetCore.Http;
    using Services;
    using System.Collections.Immutable;
    using System.IO;

    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ExcelReaderService _excelReaderService;

        [BindProperty]
        public IFormFile CatalogFile { get; set; }

        [BindProperty]
        public bool SyncProducts { get; set; }

        public EditModel(ApplicationDbContext context, ExcelReaderService excelReaderService)
        {
            _context = context;
            _excelReaderService = excelReaderService;
        }

        [BindProperty]
        public Catalog Catalog { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Catalog = await _context.Catalogs.SingleOrDefaultAsync(m => m.Id == id).ConfigureAwait(false);

            if (Catalog == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();


            if (CatalogFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    CatalogFile.CopyTo(ms);
                    Catalog.File = ms.ToArray();
                }
            }
            _context.Attach(Catalog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatalogExists(Catalog.Id)) return NotFound();
                else throw;
            }

            if (!SyncProducts) return RedirectToPage("./Index");

            _context.Products.RemoveRange(_context.Products.Where(p => p.CatalogId == Catalog.Id).Select(p => new Product { Id = p.Id }));
            await _context.SaveChangesAsync().ConfigureAwait(false);

            var rules = _context.DiscountRules.ToImmutableList();
            var products = _excelReaderService.GetLines(Catalog, rules);
            await _context.Products.AddRangeAsync(products).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return RedirectToPage("./Index");
        }

        private bool CatalogExists(int id) => _context.Catalogs.Any(e => e.Id == id);
    }
}
