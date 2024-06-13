using BusinessObjects;

namespace Repositories.Interface
{
    public interface INewsArticleRepo
    {
        Task<List<NewsArticle>> GetNewsArticlesAsync();
        Task<List<NewsArticle>> GetNewsArticlesByAccountIdAsync(short accountId);
        Task<List<NewsArticle>> GetNewsArticlesByPeriodAsync(DateTime startDate, DateTime endDate);
        Task<List<NewsArticle>> SearchNewsArticlesByTitleAsync(string title);
        Task<NewsArticle> GetNewsArticleByIdAsync(string id);
        Task DeleteNewsArticleAsync(string id);
        Task UpdateNewsArticleAsync(NewsArticle updatedNewsArticle, List<Tag> tags);
        Task AddNewsArticleAsync(NewsArticle newNewsArticle, List<Tag> tags);
    }
}
