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
                HttpContext.Session.SetString("Email", Email);

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
