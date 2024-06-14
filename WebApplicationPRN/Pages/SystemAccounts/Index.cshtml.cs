using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface;

namespace WebApplicationPRN.Pages.SystemAccounts
{
    public class IndexModel : PageModel
    {
        private readonly ISystemAccountSvc _systemAccountSvc;

        public IndexModel(ISystemAccountSvc systemAccountSvc)
        {
            _systemAccountSvc = systemAccountSvc;
        }

        [BindProperty]
        public string SearchQuery { get; set; } = "";

        public IList<SystemAccount> SystemAccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToPage("/Login");
            }

            if (HttpContext.Session.GetString("AccountId") != null)
            {
                return RedirectToPage("/Index");
            }

            SystemAccount = await _systemAccountSvc.GetAccountsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToPage("/Login");
            }

            if (HttpContext.Session.GetString("AccountId") != null)
            {
                return RedirectToPage("/Index");
            }

            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                SystemAccount = await _systemAccountSvc.GetAccountsAsync();
            }
            else
            {
                SystemAccount = await _systemAccountSvc.SearchAccountsByNameAsync(SearchQuery);
            }

            return Page();
        }
    }
}
