using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface;

namespace WebApplicationPRN.Pages.SystemAccounts
{
    public class CreateModel : PageModel
    {
        private readonly ISystemAccountSvc _systemAccountSvc;

        public CreateModel(ISystemAccountSvc systemAccountSvc)
        {
            _systemAccountSvc = systemAccountSvc;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SystemAccount SystemAccount { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _systemAccountSvc.AddSystemAccountAsync(SystemAccount);

            return RedirectToPage("./Index");
        }
    }
}
