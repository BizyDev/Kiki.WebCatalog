namespace Kiki.WebApp.Pages.Products
{
    using System.Threading.Tasks;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Product = await _context.Products
                .Include(p => p.Catalog).SingleOrDefaultAsync(m => m.Id == id).ConfigureAwait(false);

            if (Product == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            Product = await _context.Products.FindAsync(id).ConfigureAwait(false);

            if (Product != null)
            {
                _context.Products.Remove(Product);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }

            return RedirectToPage("./Index");
        }
    }
}