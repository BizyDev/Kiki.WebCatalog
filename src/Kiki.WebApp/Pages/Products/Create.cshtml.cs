namespace Kiki.WebApp.Pages.Products
{
    using System.Threading.Tasks;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CatalogId"] = new SelectList(_context.Catalogs, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _context.Products.Add(Product);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return RedirectToPage("./Index");
        }
    }
}