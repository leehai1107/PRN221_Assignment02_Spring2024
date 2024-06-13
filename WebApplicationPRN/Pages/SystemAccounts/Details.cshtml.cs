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
            else
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
                else
                {
                    SystemAccount = systemaccount;
                }
                return Page();
            }

        }
    }
}
