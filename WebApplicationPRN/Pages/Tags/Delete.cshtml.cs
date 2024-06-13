using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface;

namespace WebApplicationPRN.Pages.Tags
{
    public class DeleteModel : PageModel
    {
        private readonly ITagSvc _tagSvc;

        public DeleteModel(ITagSvc tagSvc)
        {
            _tagSvc = tagSvc;
        }

        [BindProperty]
        public Tag Tag { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _tagSvc.GetTagByIdAsync((int)id);
            if (tag != null)
            {
                Tag = tag;
                await _tagSvc.DeleteTagAsync(tag.TagId);
            }

            return RedirectToPage("./Index");
        }
    }
}
