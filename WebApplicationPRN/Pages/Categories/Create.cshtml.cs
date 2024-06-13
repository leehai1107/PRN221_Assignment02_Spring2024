using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface;

namespace WebApplicationPRN.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ICategorySvc _categorySvc;

        public CreateModel(ICategorySvc categorySvc)
        {
            _categorySvc = categorySvc;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _categorySvc.AddCategoryAsync(Category);

            return RedirectToPage("./Index");
        }
    }
}
