using BusinessObjects;

namespace Repositories.Interface
{
    public interface ITagRepo
    {
        Task AddTagAsync(Tag newTag);
        Task DeleteTagAsync(int id);
        Task<Tag> GetTagByIdAsync(int id);
        Task<List<Tag>> GetTagsAsync();
        Task UpdateTagAsync(Tag newTag);
        Task<List<Tag>> SearchTagsByNameAsync(string tagName);

    }
}
