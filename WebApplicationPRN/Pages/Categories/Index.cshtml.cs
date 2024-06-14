using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface;

namespace WebApplicationPRN.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ICategorySvc _categorySvc;

        public IndexModel(ICategorySvc categorySvc)
        {
            _categorySvc = categorySvc;
        }

        [BindProperty]
        public string SearchQuery { get; set; } = "";

        public IList<Category> Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToPage("/Login");
            }

            Category = await _categorySvc.GetCategoriesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToPage("/Login");
            }

            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                Category = await _categorySvc.GetCategoriesAsync();
            }
            else
            {
                Category = await _categorySvc.SearchCategoriesByNameAsync(SearchQuery);
            }

            return Page();
        }
    }
}
