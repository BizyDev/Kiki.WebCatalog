using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Kiki.WebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Kiki.WebApp.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace Kiki.WebApp.Pages.Users
{
    [Authorize(Roles = "Admin,Kiki")]
    public class CreateModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateModel(Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Utilisateur Utilisateur { get; set; }

        public async Task<IActionResult> OnPostAsync()      
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _userManager.CreateAsync(Utilisateur.ToApplicationUser());
            var user = await _userManager.FindByEmailAsync(Utilisateur.Email);
            var lol = await _userManager.AddPasswordAsync(user, Utilisateur.Password);
            await _userManager.AddToRoleAsync(user, "Garage");

            return RedirectToPage("./Index");
        }
    }
}