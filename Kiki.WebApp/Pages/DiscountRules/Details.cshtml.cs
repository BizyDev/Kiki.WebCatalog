using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kiki.WebApp.Data.Models;

namespace Kiki.WebApp.Pages.DiscountRules
{
    public class DetailsModel : PageModel
    {
        private readonly Kiki.WebApp.Data.ApplicationDbContext _context;

        public DetailsModel(Kiki.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
