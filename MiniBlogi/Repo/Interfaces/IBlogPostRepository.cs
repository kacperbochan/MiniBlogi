using MiniBlogi.Models;

namespace MiniBlogi.Controllers.Interfaces
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        IEnumerable<BlogPost> GetAll();
        Task<BlogPost> GetByIdAsync(int id);
        BlogPost GetById(int id);
        Task AddAsync(BlogPost blogPost);
        Task SaveChangesAsync();
        void Update(BlogPost blogPost);
        void Delete(int id);
        public void Add(BlogPost post);
        void Save();
    }

}
