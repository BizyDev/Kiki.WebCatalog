using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kiki.WebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Kiki.WebApp.Data.Models;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Kiki.WebApp.Pages.Users
{
    [Authorize(Roles = "Admin,Kiki")]
    public class EditModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User).ConfigureAwait(false);
            if (user == null)
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

            try
            {

                if (!string.IsNullOrWhiteSpace(Utilisateur.Password))
                    await _userManager.AddPasswordAsync(user, Utilisateur.Password);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilisateurExists(Utilisateur.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UtilisateurExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
