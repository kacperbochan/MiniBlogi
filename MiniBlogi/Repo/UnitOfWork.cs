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
        private ITagRepository _tagRepository;
        private IImageRepository _imageRepository;

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

        public ITagRepository TagRepository
        {
            get
            {
                if (_tagRepository == null)
                {
                    _tagRepository = new TagRepository(_context);
                }

                return _tagRepository;
            }
        }

        public IImageRepository ImageRepository
        {
            get
            {
                if (_imageRepository == null)
                {
                    _imageRepository = new ImageRepository(_context);
                }

                return _imageRepository;
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
