using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kiki.WebApp.Data;
using Kiki.WebApp.Data.Models;

namespace Kiki.WebApp.Pages.Catalogs
{
    public class DetailsModel : PageModel
    {
        private readonly Kiki.WebApp.Data.ApplicationDbContext _context;

        public DetailsModel(Kiki.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Catalog Catalog { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Catalog = await _context.Catalogs.SingleOrDefaultAsync(m => m.Id == id);

            if (Catalog == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
