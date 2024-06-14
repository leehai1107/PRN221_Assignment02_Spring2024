using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
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

        [BindProperty]
        public string SearchQuery { get; set; } = "";

        public IList<NewsArticle> NewsArticle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToPage("/Login");
            }

            NewsArticle = await _newsArticleSvc.GetNewsArticlesAsync();
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
                NewsArticle = await _newsArticleSvc.GetNewsArticlesAsync();
            }
            else
            {
                NewsArticle = await _newsArticleSvc.SearchNewsArticlesByTitleAsync(SearchQuery);
            }

            return Page();
        }
    }
}
