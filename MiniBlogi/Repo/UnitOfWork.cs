using Microsoft.EntityFrameworkCore;
using MiniBlogi.Controllers.Interfaces;
using MiniBlogi.Controllers;
using MiniBlogi.Data;
using MiniBlogi.Repo.Interfaces;

namespace MiniBlogi.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogDbContext _context;
        private IBlogPostRepository _blogPostRepository;
        private ICommentRepository _commentRepository;

        public UnitOfWork(BlogDbContext context)
        {
            _context = context;
        }

        public IBlogPostRepository BlogPostRepository
        {
            get
            {
                if (_blogPostRepository == null)
                {
                    _blogPostRepository = new BlogPostRepository(_context);
                }

                return _blogPostRepository;
            }
        }

        public ICommentRepository CommentRepository
        {
            get
            {
                if (_commentRepository == null)
                {
                    _commentRepository = new CommentRepository(_context);
                }

                return _commentRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
