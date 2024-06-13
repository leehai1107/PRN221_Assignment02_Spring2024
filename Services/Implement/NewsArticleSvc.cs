using BusinessObjects;
using Repositories.Interface;
using Services.Interface;

namespace Services.Implement
{
    public class NewsArticleSvc : INewsArticleSvc
    {
        private readonly INewsArticleRepo _newsArticleRepo;
        public NewsArticleSvc(INewsArticleRepo newsArticleRepo)
        {
            _newsArticleRepo = newsArticleRepo;
        }

        public async Task AddNewsArticleAsync(NewsArticle newNewsArticle, List<Tag> tags)
        {
            await _newsArticleRepo.AddNewsArticleAsync(newNewsArticle, tags);
        }

        public async Task DeleteNewsArticleAsync(string id)
        {
            await _newsArticleRepo.DeleteNewsArticleAsync(id);
        }

        public async Task<List<NewsArticle>> GetNewsArticlesAsync()
        {
            return await _newsArticleRepo.GetNewsArticlesAsync();
        }

        public async Task<List<NewsArticle>> GetNewsArticlesByAccountIdAsync(short accountId)
        {
            return await _newsArticleRepo.GetNewsArticlesByAccountIdAsync(accountId);
        }

        public async Task UpdateNewsArticleAsync(NewsArticle updatedNewsArticle, List<Tag> tags)
        {
            await _newsArticleRepo.UpdateNewsArticleAsync(updatedNewsArticle, tags);
        }

        public async Task<List<NewsArticle>> GetNewsArticlesByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _newsArticleRepo.GetNewsArticlesByPeriodAsync(startDate, endDate);
        }

        public async Task<List<NewsArticle>> SearchNewsArticlesByTitleAsync(string title)
        {
            return await _newsArticleRepo.SearchNewsArticlesByTitleAsync(title);
        }

        public async Task<NewsArticle> GetNewsArticleByIdAsync(string id)
        {
            return await _newsArticleRepo.GetNewsArticleByIdAsync(id);
        }

        public async Task<List<NewsArticle>> GetNewsArticlesActiveAsync()
        {
            return await _newsArticleRepo.GetNewsArticlesActiveAsync();
        }
    }
}
