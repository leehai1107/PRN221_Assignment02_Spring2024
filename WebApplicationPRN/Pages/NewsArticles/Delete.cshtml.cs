using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface;

namespace WebApplicationPRN.Pages.NewsArticles
{
    public class DeleteModel : PageModel
    {
        private readonly INewsArticleSvc _newsArticleSvc;

        public DeleteModel(INewsArticleSvc newsArticleSvc)
        {
            _newsArticleSvc = newsArticleSvc;
        }

        [BindProperty]
        public NewsArticle NewsArticle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var newsarticle = await _context.NewsArticles.FirstOrDefaultAsync(m => m.NewsArticleId == id);
            var newsarticle = _newsArticleSvc.GetNewsArticleByIdAsync(id);

            if (newsarticle == null)
            {
                return NotFound();
            }
            else
            {
                NewsArticle = await newsarticle;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsarticle = _newsArticleSvc.GetNewsArticleByIdAsync(id);
            if (newsarticle != null)
            {
                NewsArticle = await newsarticle;
                await _newsArticleSvc.DeleteNewsArticleAsync(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
