using BusinessObjects;
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

        public IList<Tag> Tag { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Tag = await _tagSvc.GetTagsAsync();
        }
    }
}
