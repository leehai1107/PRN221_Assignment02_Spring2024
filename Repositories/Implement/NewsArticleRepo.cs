using BusinessObjects;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;

namespace Repositories.Implement
{
    public class NewsArticleRepo : INewsArticleRepo
    {
        public async Task AddNewsArticleAsync(NewsArticle newNewsArticle, List<Tag> tags)
        {
            using (FunewsManagementDbContext _context = new FunewsManagementDbContext())
            {
                foreach (var tag in tags)
                {
                    var existingTag = await _context.Tags.FirstOrDefaultAsync(t => t.TagId == tag.TagId);
                    if (existingTag != null)
                    {
                        newNewsArticle.Tags.Add(existingTag);
                    }
                    else
                    {
                        _context.Tags.Add(tag);
                        newNewsArticle.Tags.Add(tag);
                    }
                }

                _context.NewsArticles.Add(newNewsArticle);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteNewsArticleAsync(string id)
        {
            using (FunewsManagementDbContext _context = new())
            {
                var existData = await _context.NewsArticles.Include(x => x.Tags).FirstOrDefaultAsync(x => x.NewsArticleId == id);
                if (existData != null)
                {
                    existData.Tags.Clear();
                    _context.NewsArticles.Update(existData);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("NewsArticle not found", nameof(id));
                }
            }

            using (FunewsManagementDbContext _context = new())
            {
                var existData = await _context.NewsArticles.FindAsync(id);
                if (existData != null)
                {
                    _context.NewsArticles.Remove(existData);
                    await _context.SaveChangesAsync();
                    _context.Entry(existData).State = EntityState.Detached;

                }
                else
                {
                    throw new ArgumentException("NewsArticle not found", nameof(id));
                }
            }
        }

        public async Task<List<NewsArticle>> GetNewsArticlesAsync()
        {
            using (FunewsManagementDbContext _context = new())
            {
                return await _context.NewsArticles
                    .Include(x => x.Tags)
                    .Include(z => z.Category)
                    .Include(y => y.CreatedBy)
                    .ToListAsync();
            }
        }

        public async Task<List<NewsArticle>> GetNewsArticlesByAccountIdAsync(short accountId)
        {
            using (FunewsManagementDbContext _context = new())
            {
                return await _context.NewsArticles
                    .Include(x => x.Tags)
                    .Include(z => z.Category)
                    .Include(y => y.CreatedBy)
                    .Where(x => x.CreatedById == accountId)
                    .ToListAsync();
            }
        }

        public async Task UpdateNewsArticleAsync(NewsArticle updatedNewsArticle, List<Tag> tags)
        {
            using (FunewsManagementDbContext _context = new FunewsManagementDbContext())
            {
                var existingNewsArticle = await _context.NewsArticles
                    .Include(n => n.Tags)
                    .FirstOrDefaultAsync(n => n.NewsArticleId == updatedNewsArticle.NewsArticleId);

                if (existingNewsArticle != null)
                {
                    existingNewsArticle.Tags.Clear();

                    existingNewsArticle.NewsTitle = updatedNewsArticle.NewsTitle;
                    existingNewsArticle.NewsContent = updatedNewsArticle.NewsContent;
                    existingNewsArticle.CreatedDate = updatedNewsArticle.CreatedDate;
                    existingNewsArticle.CategoryId = updatedNewsArticle.CategoryId;
                    existingNewsArticle.CreatedDate = updatedNewsArticle.CreatedDate;
                    existingNewsArticle.ModifiedDate = updatedNewsArticle.ModifiedDate;
                    existingNewsArticle.CreatedById = updatedNewsArticle.CreatedById;
                    existingNewsArticle.NewsStatus = updatedNewsArticle.NewsStatus;

                    foreach (var tag in tags)
                    {
                        var existingTag = await _context.Tags.FirstOrDefaultAsync(t => t.TagId == tag.TagId);
                        if (existingTag != null)
                        {
                            existingNewsArticle.Tags.Add(existingTag);
                        }
                        else
                        {
                            _context.Tags.Add(tag);
                            existingNewsArticle.Tags.Add(tag);
                        }
                    }

                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<List<NewsArticle>> GetNewsArticlesByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            using (FunewsManagementDbContext _context = new FunewsManagementDbContext())
            {
                return await _context.NewsArticles
                    .Include(x => x.Tags)
                    .Include(z => z.Category)
                    .Include(y => y.CreatedBy)
                    .Where(x => x.CreatedDate >= startDate && x.CreatedDate <= endDate)
                    .ToListAsync();
            }
        }

        public async Task<List<NewsArticle>> SearchNewsArticlesByTitleAsync(string title)
        {
            using (FunewsManagementDbContext _context = new())
            {
                return await _context.NewsArticles
                    .Include(x => x.Tags)
                    .Include(z => z.Category)
                    .Include(y => y.CreatedBy)
                    .Where(n => n.NewsTitle.Contains(title))
                    .ToListAsync();
            }
        }

        public async Task<NewsArticle> GetNewsArticleByIdAsync(string id)
        {
            using (FunewsManagementDbContext _context = new())
            {
                return await _context.NewsArticles.Include(x => x.Tags)
                    .Include(z => z.Category)
                    .Include(y => y.CreatedBy).FirstOrDefaultAsync(x => x.NewsArticleId == id);
            }
        }

        public async Task<List<NewsArticle>> GetNewsArticlesActiveAsync()
        {
            using (FunewsManagementDbContext _context = new())
            {
                return await _context.NewsArticles
                    .Include(x => x.Tags)
                    .Include(z => z.Category)
                    .Include(y => y.CreatedBy)
                    .Where(x => x.NewsStatus == true)
                    .ToListAsync();
            }
        }
    }
}
