using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kiki.WebApp.Data;
using Kiki.WebApp.Data.Models;

namespace Kiki.WebApp.Pages.DiscountRules
{
    public class DeleteModel : PageModel
    {
        private readonly Kiki.WebApp.Data.ApplicationDbContext _context;

        public DeleteModel(Kiki.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DiscountRule DiscountRule { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DiscountRule = await _context.DiscountRules.FirstOrDefaultAsync(m => m.Id == id);

            if (DiscountRule == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DiscountRule = await _context.DiscountRules.FindAsync(id);

            if (DiscountRule != null)
            {
                _context.DiscountRules.Remove(DiscountRule);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
