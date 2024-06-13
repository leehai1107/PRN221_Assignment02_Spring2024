using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationPRN.Pages.NewsArticles
{
    public class DetailsModel : PageModel
    {
        private readonly DataAccessLayer.FunewsManagementDbContext _context;

        public DetailsModel(DataAccessLayer.FunewsManagementDbContext context)
        {
            _context = context;
        }

        public NewsArticle NewsArticle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
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

                var newsarticle = await _context.NewsArticles.Include(x => x.Category).Include(x => x.CreatedBy).FirstOrDefaultAsync(m => m.NewsArticleId == id);
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
}
