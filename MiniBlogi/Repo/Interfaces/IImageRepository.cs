using MiniBlogi.Models;

namespace MiniBlogi.Repo.Interfaces
{
    public interface IImageRepository
    {
        Task<IEnumerable<Image>> GetAllAsync();
        Task<Image> GetByIdAsync(int id);
        Task<Image?> GetByDataAsync(Image image);
        Task<Image?> GetByDataAsync(string name);
        bool IsImageWithData(Image image);
        Task AddAsync(Image blogPost);
        Task UpdateAsync(Image blogPost);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}
