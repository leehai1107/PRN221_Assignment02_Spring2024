using BusinessObjects;
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


        public IList<Category> Category { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Category = await _categorySvc.GetCategoriesAsync();

            if (HttpContext.Session.GetString("Email") == null)
            {
                Response.Redirect("/Login");
            }

        }
    }
}
