using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface;

namespace LeChiHaiRazorPages.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ISystemAccountSvc _systemAccountSvc;

        public LoginModel(ISystemAccountSvc systemAccountSvc)
        {
            _systemAccountSvc = systemAccountSvc;
        }

        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public async Task<IActionResult> OnPost()
        {
            if (await _systemAccountSvc.ValidateAsync(Email, Password))
            {
                var systemAccount = await _systemAccountSvc.GetAccountByEmailAsync(Email);

                HttpContext.Session.SetString("Email", Email);
                if (!_systemAccountSvc.SignUpWithAdminAccount(Email))
                {
                    HttpContext.Session.SetString("AccountId", systemAccount.AccountId.ToString());
                }

                // Redirect to the home page or dashboard
                return RedirectToPage("/Index");
            }
            else
            {
                // Add an error message
                ModelState.AddModelError("", "You have no permission to this system!");
                return Page();
            }
        }
    }
}
