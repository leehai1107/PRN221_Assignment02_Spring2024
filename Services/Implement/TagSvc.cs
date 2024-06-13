using BusinessObjects;
using Repositories.Interface;
using Services.Interface;

namespace Services.Implement
{
    public class TagSvc : ITagSvc
    {
        private readonly ITagRepo _tagRepo;
        public TagSvc(ITagRepo tagRepo)
        {
            _tagRepo = tagRepo;
        }

        public async Task AddTagAsync(Tag newTag)
        {
            await _tagRepo.AddTagAsync(newTag);
        }

        public async Task DeleteTagAsync(int id)
        {
            await _tagRepo.DeleteTagAsync(id);
        }

        public async Task<Tag> GetTagByIdAsync(int id)
        {
            return await _tagRepo.GetTagByIdAsync(id);
        }

        public async Task<List<Tag>> GetTagsAsync()
        {
            return await _tagRepo.GetTagsAsync();
        }

        public async Task UpdateTagAsync(Tag newTag)
        {
            await _tagRepo.UpdateTagAsync(newTag);
        }

        public async Task<List<Tag>> SearchTagsByNameAsync(string tagName)
        {
            return await _tagRepo.SearchTagsByNameAsync(tagName);
        }
    }
}
