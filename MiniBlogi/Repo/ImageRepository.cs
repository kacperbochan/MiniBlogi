using Microsoft.EntityFrameworkCore;
using MiniBlogi.Data;
using MiniBlogi.Models;
using MiniBlogi.Repo.Interfaces;

namespace MiniBlogi.Repo
{
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        // Dodatkowe metody specyficzne dla Comment
        public ImageRepository(BlogDbContext context) : base(context)
        {
        }

        public Task<Image?> GetByDataAsync(Image image)
        {
            return DbSet.Where(x => 
                x.FilePath == image.FilePath &&
                x.FileFormat == image.FileFormat &&
                x.FileName == x.FileName).FirstOrDefaultAsync();
        }

        public Task<Image?> GetByDataAsync(string name)
        {
            return DbSet.Where(x =>
                x.FileName == name).FirstOrDefaultAsync();
        }

        public bool IsImageWithData(Image image)
        {
            return DbSet.Any(x =>
                x.FilePath == image.FilePath &&
                x.FileFormat == image.FileFormat &&
                x.FileName == x.FileName);
        }
    }

}
