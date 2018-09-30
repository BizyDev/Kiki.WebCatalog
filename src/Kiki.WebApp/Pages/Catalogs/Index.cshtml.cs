namespace Kiki.WebApp.Pages.Catalogs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    [Authorize(Roles = "Admin,Kiki")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Catalog> Catalog { get; set; }

        public async Task OnGetAsync()
        {
            Catalog = await _context.Catalogs.ToListAsync().ConfigureAwait(false);
        }
    }
}