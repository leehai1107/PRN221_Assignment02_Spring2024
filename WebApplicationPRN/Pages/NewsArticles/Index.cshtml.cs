using BusinessObjects;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface;

namespace WebApplicationPRN.Pages.NewsArticles
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
            NewsArticle = await _newsArticleSvc.GetNewsArticlesAsync();
            if (HttpContext.Session.GetString("Email") == null)
            {
                Response.Redirect("/Login");
            }
        }
    }
}
