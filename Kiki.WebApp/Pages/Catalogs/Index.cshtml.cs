using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kiki.WebApp.Data.Models;

namespace Kiki.WebApp.Pages.Catalogs
{
    public class IndexModel : PageModel
    {
        private readonly Kiki.WebApp.Data.ApplicationDbContext _context;

        public IndexModel(Kiki.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Catalog> Catalog { get;set; }

        public async Task OnGetAsync()
        {
            Catalog = await _context.Catalogs.ToListAsync();
        }
    }
}
