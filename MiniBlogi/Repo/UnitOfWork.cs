using Microsoft.EntityFrameworkCore;

namespace MiniBlogi.Repo
{
    public class UnitOfWork : IDisposable
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
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
