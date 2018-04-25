namespace Kiki.WebApp.Pages.Account.Manage
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class ShowRecoveryCodesModel : PageModel
    {
        public string[] RecoveryCodes { get; private set; }

        public IActionResult OnGet()
        {
            RecoveryCodes = (string[]) TempData["RecoveryCodes"];
            if (RecoveryCodes == null) return RedirectToPage("TwoFactorAuthentication");

            return Page();
        }
    }
}