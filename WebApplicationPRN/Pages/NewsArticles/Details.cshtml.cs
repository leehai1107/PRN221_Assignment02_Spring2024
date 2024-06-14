using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface;

namespace WebApplicationPRN.Pages.NewsArticles
{
    public class DetailsModel : PageModel
    {
        private readonly INewsArticleSvc _newsArticleSvc;

        public DetailsModel(INewsArticleSvc newsArticleSvc)
        {
            _newsArticleSvc = newsArticleSvc;
        }

        public NewsArticle NewsArticle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsarticle = await _newsArticleSvc.GetNewsArticleByIdAsync(id);
            if (newsarticle == null)
            {
                return NotFound();
            }
            else
            {
                NewsArticle = newsarticle;
            }
            return Page();
        }
    }
}
