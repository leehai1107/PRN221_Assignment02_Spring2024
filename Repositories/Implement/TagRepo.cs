using BusinessObjects;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;

namespace Repositories.Implement
{
    public class TagRepo : ITagRepo
    {
        public async Task AddTagAsync(Tag newTag)
        {
            using (FunewsManagementDbContext _context = new())
            {
                try
                {
                    _context.Tags.Add(newTag);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception("Can not add this tag", ex);
                }
            }
        }

        public async Task DeleteTagAsync(int id)
        {
            using (FunewsManagementDbContext _context = new())
            {
                var existTag = await _context.Tags.FindAsync(id);
                if (existTag != null)
                {
                    try
                    {
                        _context.Tags.Remove(existTag);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Can not delete this tag", ex);
                    }
                }
                else
                {
                    throw new ArgumentException("Tag not found", nameof(id));
                }
            }
        }

        public async Task<Tag> GetTagByIdAsync(int id)
        {
            using (FunewsManagementDbContext _context = new())
            {
                return await _context.Tags.FindAsync(id);
            }
        }

        public async Task<List<Tag>> GetTagsAsync()
        {
            using (FunewsManagementDbContext _context = new())
            {
                return await _context.Tags.ToListAsync();
            }
        }

        public async Task UpdateTagAsync(Tag newTag)
        {
            using (FunewsManagementDbContext _context = new())
            {
                var existTag = await _context.Tags.FindAsync(newTag.TagId);
                if (existTag != null)
                {
                    try
                    {
                        existTag.TagName = newTag.TagName;
                        existTag.Note = newTag.Note;
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Can not update this tag", ex);
                    }
                }
                else
                {
                    throw new ArgumentException("Tag not found", nameof(newTag.TagId));
                }
            }
        }

        public async Task<List<Tag>> SearchTagsByNameAsync(string tagName)
        {
            using (FunewsManagementDbContext _context = new())
            {
                return await _context.Tags
                               .Where(t => t.TagName.Contains(tagName))
                               .ToListAsync();
            }
        }
    }
}
