using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface;

namespace WebApplicationPRN.Pages.Tags
{
    public class DetailsModel : PageModel
    {
        private readonly ITagSvc _tagSvc;

        public DetailsModel(ITagSvc tagSvc)
        {
            _tagSvc = tagSvc;
        }

        public Tag Tag { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
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

                var tag = await _tagSvc.GetTagByIdAsync((int)id);
                if (tag == null)
                {
                    return NotFound();
                }
                else
                {
                    Tag = tag;
                }
                return Page();
            }

        }
    }
}
