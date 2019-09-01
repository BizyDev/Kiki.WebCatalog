namespace Kiki.WebApp.Pages.RulesBatch
{
    using System.Collections.Immutable;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Services;

    [Authorize(Roles = "Admin,Kiki")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IExcelReaderService _excelReaderService;

        [BindProperty]
        public IFormFile RulesFile { get; set; }

        [BindProperty]
        public bool DeletePrevious { get; set; }

        public CreateModel(ApplicationDbContext context, IExcelReaderService excelReaderService)
        {
            _context = context;
            _excelReaderService = excelReaderService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            if (RulesFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    RulesFile.CopyTo(ms);
                    var file = ms.ToArray();


                    if (!DeletePrevious)
                    {
                        var previous = _context.DiscountRules.Select(d => new DiscountRule { Id = d.Id });
                        _context.DiscountRules.RemoveRange(previous);
                        await _context.SaveChangesAsync().ConfigureAwait(false);
                    }

                    var rules = _excelReaderService.GetRules(file).ToList();
                    await _context.DiscountRules.AddRangeAsync(rules).ConfigureAwait(false);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
            }
            return RedirectToPage("./Index");
        }
    }
}