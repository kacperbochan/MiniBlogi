using Microsoft.EntityFrameworkCore;
using MiniBlogi.Data;

namespace MiniBlogi.Repo
{
    public class UnitOfWork : IDisposable
    {
        private readonly BlogDbContext _context;

        public UnitOfWork(BlogDbContext context)
        {
            _context = context;
        }

        public BlogDbContext Context { 
            get { return _context; } 
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
