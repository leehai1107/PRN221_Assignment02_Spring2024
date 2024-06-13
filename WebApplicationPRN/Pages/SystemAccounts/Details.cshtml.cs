using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface;

namespace WebApplicationPRN.Pages.SystemAccounts
{
    public class DetailsModel : PageModel
    {
        private readonly ISystemAccountSvc _systemAccountSvc;

        public DetailsModel(ISystemAccountSvc systemAccountSvc)
        {
            _systemAccountSvc = systemAccountSvc;
        }

        public SystemAccount SystemAccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToPage("/Index");

            }
            // Check is admin or not
            if (HttpContext.Session.GetString("AccountId") == null)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var systemaccount = await _systemAccountSvc.GetSystemAccountByIdAsync((short)id);
                if (systemaccount == null)
                {
                    return NotFound();
                }
                SystemAccount = systemaccount;
                return Page();
            }
            // If not admin
            else
            {
                if (HttpContext.Session.GetString("AccountId").ToString() != id.ToString())
                {
                    return RedirectToPage("/Index");
                }

                if (id == null)
                {
                    return NotFound();
                }

                var systemaccount = await _systemAccountSvc.GetSystemAccountByIdAsync((short)id);
                if (systemaccount == null)
                {
                    return NotFound();
                }
                else
                {
                    SystemAccount = systemaccount;
                }
                return Page();
            }

        }
    }
}
