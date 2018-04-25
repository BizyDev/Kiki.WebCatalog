namespace Kiki.WebApp.Pages.Catalogs
{
    using System.Threading.Tasks;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Catalog Catalog { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Catalog = await _context.Catalogs.SingleOrDefaultAsync(m => m.Id == id).ConfigureAwait(false);

            if (Catalog == null) return NotFound();
            return Page();
        }
    }
}