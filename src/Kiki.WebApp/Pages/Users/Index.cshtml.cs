using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kiki.WebApp.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Kiki.WebApp.Pages.Users
{
    [Authorize(Roles = "Admin,Kiki")]
    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public IndexModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Utilisateur> Utilisateur { get;set; }

        public async Task OnGetAsync()
        {
            Utilisateur = (await _context.Users.ToListAsync()).Select(u => u.ToUtilisateur()).ToList();
        }
    }
}
