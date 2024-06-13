using BusinessObjects;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface;

namespace WebApplicationPRN.Pages
{
    public class IndexModel : PageModel
    {
        private readonly INewsArticleSvc _newsArticleSvc;

        public IndexModel(INewsArticleSvc newsArticleSvc)
        {
            _newsArticleSvc = newsArticleSvc;
        }

        public IList<NewsArticle> NewsArticle { get; set; } = default!;

        public async Task OnGetAsync()
        {
            NewsArticle = await _newsArticleSvc.GetNewsArticlesActiveAsync();
        }
    }
}
