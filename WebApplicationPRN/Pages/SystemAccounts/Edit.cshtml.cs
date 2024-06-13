using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Services.Interface;

namespace WebApplicationPRN.Pages.SystemAccounts
{
    public class EditModel : PageModel
    {
        private readonly ISystemAccountSvc _systemAccountSvc;

        public EditModel(ISystemAccountSvc systemAccountSvc)
        {
            _systemAccountSvc = systemAccountSvc;
        }

        [BindProperty]
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

                // Check not user can edit another with above check
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
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            try
            {
                await _systemAccountSvc.UpdateSystemAccountAsync(SystemAccount);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SystemAccountExists(SystemAccount.AccountId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SystemAccountExists(short id)
        {
            return _systemAccountSvc.GetSystemAccountByIdAsync(id) != null;
        }
    }
}
