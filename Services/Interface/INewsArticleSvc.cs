using BusinessObjects;

namespace Services.Interface
{
    public interface INewsArticleSvc
    {
        Task AddNewsArticleAsync(NewsArticle newNewsArticle, List<Tag> tags);
        Task DeleteNewsArticleAsync(string id);
        Task<List<NewsArticle>> GetNewsArticlesAsync();
        Task<List<NewsArticle>> GetNewsArticlesByAccountIdAsync(short accountId);
        Task UpdateNewsArticleAsync(NewsArticle updatedNewsArticle, List<Tag> tags);
        Task<List<NewsArticle>> GetNewsArticlesByPeriodAsync(DateTime startDate, DateTime endDate);
        Task<List<NewsArticle>> SearchNewsArticlesByTitleAsync(string title);
        Task<NewsArticle> GetNewsArticleByIdAsync(string id);
        Task<List<NewsArticle>> GetNewsArticlesActiveAsync();

    }
}
