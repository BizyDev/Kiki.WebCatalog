using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Kiki.WebApp.Data;
using Kiki.WebApp.Data.Models;

namespace Kiki.WebApp.Pages.Catalogs
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

            _context.Catalogs.Add(Catalog);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}