using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Services.Interface;

namespace WebApplicationPRN.Pages.NewsArticles
{
    public class EditModel : PageModel
    {
        private readonly ICategorySvc _categorySvc;
        private readonly ISystemAccountSvc _systemAccountSvc;
        private readonly ITagSvc _tagSvc;
        private readonly INewsArticleSvc _newsArticleSvc;

        public EditModel(ICategorySvc categorySvc, ISystemAccountSvc systemAccountSvc, ITagSvc tagSvc, INewsArticleSvc newsArticleSvc)
        {
            _categorySvc = categorySvc;
            _systemAccountSvc = systemAccountSvc;
            _tagSvc = tagSvc;
            _newsArticleSvc = newsArticleSvc;
        }

        [BindProperty]
        public NewsArticle NewsArticle { get; set; } = default!;

        [BindProperty]
        public int[] TagsIdSelected { get; set; }

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

                var newsarticle = await _newsArticleSvc.GetNewsArticleByIdAsync(id);

                if (newsarticle == null)
                {
                    return NotFound();
                }
                NewsArticle = newsarticle;

                // Retrieve all available tags
                ViewData["Tags"] = new MultiSelectList(await _tagSvc.GetTagsAsync(), "TagId", "TagName");

                // Set selected tags
                ViewData["SelectedTags"] = NewsArticle.Tags.ToList();

                ViewData["CategoryId"] = new SelectList(await _categorySvc.GetCategoriesAsync(), "CategoryId", "CategoryName");
                ViewData["CreatedById"] = new SelectList(await _systemAccountSvc.GetAccountsAsync(), "AccountId", "AccountName");
                return Page();
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {

                List<Tag> tags = new List<Tag>();

                foreach (var tagId in TagsIdSelected)
                {
                    var tag = await _tagSvc.GetTagByIdAsync(tagId);
                    tags.Add(tag);
                }

                NewsArticle.ModifiedDate = DateTime.Now;

                await _newsArticleSvc.UpdateNewsArticleAsync(NewsArticle, tags);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsArticleExists(NewsArticle.NewsArticleId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool NewsArticleExists(string id)
        {
            return _newsArticleSvc.GetNewsArticleByIdAsync(id) != null;
        }
    }
}
