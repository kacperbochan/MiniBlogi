using Microsoft.EntityFrameworkCore;
using MiniBlogi.Data;
using MiniBlogi.Models;
using MiniBlogi.Repo.Interfaces;

namespace MiniBlogi.Repo
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        // Dodatkowe metody specyficzne dla Tag
        public TagRepository(BlogDbContext context) : base(context)
        {
        }

        public async Task<Tag> GetByNameAsync(string name)
        {
            return await DbSet.Where(x => x.Name == name).FirstOrDefaultAsync();
        }

        public bool IsTagWithName(string name)
        {
            return DbSet.Any(x => x.Name == name);
        }
    }

}
