using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface;

namespace WebApplicationPRN.Pages.Categories
{
    public class DetailsModel : PageModel
    {
        private readonly ICategorySvc _categorySvc;

        public DetailsModel(ICategorySvc categorySvc)
        {
            _categorySvc = categorySvc;
        }

        public Category Category { get; set; } = default!;

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
        }
    }
}
