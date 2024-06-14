using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface;

namespace LeChiHaiRazorPages.Pages.NewsArticles
{
    public class HistoryModel : PageModel
    {
        private readonly INewsArticleSvc _newsArticleSvc;
        private readonly ISystemAccountSvc _systemAccountSvc;

        public HistoryModel(INewsArticleSvc newsArticleSvc, ISystemAccountSvc systemAccountSvc)
        {
            _newsArticleSvc = newsArticleSvc;
            _systemAccountSvc = systemAccountSvc;
        }

        public IList<NewsArticle> NewsArticle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToPage("/Index");
            }

            // Check is admin or not
            if (HttpContext.Session.GetString("AccountId") == null)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var systemaccount = await _systemAccountSvc.GetSystemAccountByIdAsync(Convert.ToInt16(id));
                if (systemaccount == null)
                {
                    return NotFound();
                }
                return Page();
            }
            // If not admin
            else
            {
                if (HttpContext.Session.GetString("AccountId").ToString() != id.ToString())
                {
                    return RedirectToPage("./Index");
                }

                if (id == null)
                {
                    return NotFound();
                }
                NewsArticle = await _newsArticleSvc.GetNewsArticlesByAccountIdAsync(Convert.ToInt16(id));
                return Page();

            }

        }
    }
}
