﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Kiki.WebApp.Data.Models;

namespace Kiki.WebApp.Pages.DiscountRules
{
    public class CreateModel : PageModel
    {
        private readonly Kiki.WebApp.Data.ApplicationDbContext _context;

        public CreateModel(Kiki.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DiscountRule DiscountRule { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.DiscountRules.Add(DiscountRule);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}