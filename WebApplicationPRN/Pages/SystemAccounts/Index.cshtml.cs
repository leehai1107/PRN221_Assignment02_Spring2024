using BusinessObjects;
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


        public IList<SystemAccount> SystemAccount { get; set; } = default!;

        public async Task OnGetAsync()
        {
            SystemAccount = await _systemAccountSvc.GetAccountsAsync();
        }
    }
}
