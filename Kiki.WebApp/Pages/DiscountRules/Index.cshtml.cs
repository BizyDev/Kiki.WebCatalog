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
    public class IndexModel : PageModel
    {
        private readonly Kiki.WebApp.Data.ApplicationDbContext _context;

        public IndexModel(Kiki.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<DiscountRule> DiscountRule { get;set; }

        public async Task OnGetAsync()
        {
            DiscountRule = await _context.DiscountRules.ToListAsync();
        }
    }
}
