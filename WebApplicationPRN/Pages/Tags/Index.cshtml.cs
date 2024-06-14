using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface;

namespace WebApplicationPRN.Pages.Tags
{
    public class IndexModel : PageModel
    {
        private readonly ITagSvc _tagSvc;

        public IndexModel(ITagSvc tagSvc)
        {
            _tagSvc = tagSvc;
        }

        [BindProperty]
        public string SearchQuery { get; set; } = "";

        public IList<Tag> Tag { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToPage("/Login");
            }

            Tag = await _tagSvc.GetTagsAsync();
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
                Tag = await _tagSvc.GetTagsAsync();
            }
            else
            {
                Tag = await _tagSvc.SearchTagsByNameAsync(SearchQuery);
            }

            return Page();
        }
    }
}
