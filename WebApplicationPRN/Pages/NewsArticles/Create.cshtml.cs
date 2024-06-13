using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Interface;
using System.Diagnostics;

namespace WebApplicationPRN.Pages.NewsArticles
{
    public class CreateModel : PageModel
    {
        private readonly INewsArticleSvc _newsArticleSvc;
        private readonly ICategorySvc _categorySvc;
        private readonly ISystemAccountSvc _systemAccountSvc;
        private readonly ITagSvc _tagSvc;

        public CreateModel(INewsArticleSvc newsArticleSvc, ICategorySvc categorySvc, ISystemAccountSvc systemAccountSvc, ITagSvc tagSvc)
        {
            _newsArticleSvc = newsArticleSvc;
            _categorySvc = categorySvc;
            _systemAccountSvc = systemAccountSvc;
            _tagSvc = tagSvc;
        }

        public async Task<IActionResult> OnGet()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToPage("/Index");

            }
            else
            {
                ViewData["Tags"] = new MultiSelectList(await _tagSvc.GetTagsAsync(), "TagId", "TagName");

                ViewData["CategoryId"] = new SelectList(await _categorySvc.GetCategoriesAsync(), "CategoryId", "CategoryName");
                ViewData["CreatedById"] = new SelectList(await _systemAccountSvc.GetAccountsAsync(), "AccountId", "AccountName");
                return Page();
            }
        }
        [BindProperty]
        public NewsArticle NewsArticle { get; set; } = default!;
        [BindProperty]
        public List<Tag> Tags { get; set; } = new List<Tag>();


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Debug.WriteLine(NewsArticle.Tags.Count);
            await _newsArticleSvc.AddNewsArticleAsync(NewsArticle, Tags);

            return (RedirectToPage("./Index"));
        }
    }
}
