using BusinessObjects;

namespace Services.Interface
{
    public interface ITagSvc
    {
        Task AddTagAsync(Tag newTag);
        Task DeleteTagAsync(int id);
        Task<Tag> GetTagByIdAsync(int id);
        Task<List<Tag>> GetTagsAsync();
        Task UpdateTagAsync(Tag newTag);
        Task<List<Tag>> SearchTagsByNameAsync(string tagName);

    }
}
