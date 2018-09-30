using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kiki.WebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Kiki.WebApp.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace Kiki.WebApp.Pages.Users
{
    [Authorize(Roles = "Admin,Kiki")]
    public class DetailsModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DetailsModel(Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Utilisateur Utilisateur { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Utilisateur = (await _context.Users.SingleOrDefaultAsync(m => m.Id == id)).ToUtilisateur();

            if (Utilisateur == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
