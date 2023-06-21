using MiniBlogi.Models;

namespace MiniBlogi.Repo.Interfaces
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag> GetByIdAsync(int id);
        Task<Tag> GetByNameAsync(string name);
        bool IsTagWithName(string name);
        Task AddAsync(Tag blogPost);
        Task UpdateAsync(Tag blogPost);
        Task DeleteAsync(int id);
        int Count(int id);
        Task SaveAsync();
    }
}
