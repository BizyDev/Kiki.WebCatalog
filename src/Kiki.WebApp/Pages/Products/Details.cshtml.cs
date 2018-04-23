﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kiki.WebApp.Data;
using Kiki.WebApp.Data.Models;

namespace Kiki.WebApp.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products
                .Include(p => p.Catalog).SingleOrDefaultAsync(m => m.Id == id).ConfigureAwait(false);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
