using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Kiki.WebApp.Data;
using Kiki.WebApp.Data.Models;

namespace Kiki.WebApp.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly Kiki.WebApp.Data.ApplicationDbContext _context;

        public CreateModel(Kiki.WebApp.Data.ApplicationDbContext context)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}