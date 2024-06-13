using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface;

namespace WebApplicationPRN.Pages.Tags
{
    public class CreateModel : PageModel
    {
        private readonly ITagSvc _tagSvc;

        public CreateModel(ITagSvc tagSvc)
        {
            _tagSvc = tagSvc;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToPage("/Index");

            }
            else
            { return Page(); }
        }

        [BindProperty]
        public Tag Tag { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _tagSvc.AddTagAsync(Tag);

            return RedirectToPage("./Index");
        }
    }
}
