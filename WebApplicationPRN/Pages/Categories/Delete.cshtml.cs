using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface;

namespace WebApplicationPRN.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ICategorySvc _categorySvc;

        public DeleteModel(ICategorySvc categorySvc)
        {
            _categorySvc = categorySvc;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categorySvc.GetCategoryByIdAsync((short)id);

            if (category == null)
            {
                return NotFound();
            }
            else
            {
                Category = category;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categorySvc.GetCategoryByIdAsync((short)id);
            if (category != null)
            {
                Category = category;
                await _categorySvc.GetCategoryByIdAsync(Category.CategoryId);
            }

            return RedirectToPage("./Index");
        }
    }
}
