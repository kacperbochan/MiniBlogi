using MiniBlogi.Models;

namespace MiniBlogi.Controllers.Interfaces
{
    public interface IBlogPostRepository
    {
        Task<bool> IsBlogNotNull();
        Task<int> GetPageAmount();
        Task<int> GetUserPageAmount(string userId);
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<IEnumerable<BlogPost>> GetCurrentPage(int currentPage);
        Task<IEnumerable<BlogPost>> GetCurrentPageOfUser(int currentPage, string UserId);
        Task<BlogPost?> GetByIdAsync(int id);
        Task AddAsync(BlogPost blogPost);
        Task UpdateAsync(BlogPost blogPost);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }

}
