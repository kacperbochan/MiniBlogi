using Microsoft.EntityFrameworkCore;
using MiniBlogi.Controllers.Interfaces;
using MiniBlogi.Data;
using MiniBlogi.Models;

namespace MiniBlogi.Controllers
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogDbContext _context;

        public BlogPostRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _context.BlogPosts.ToListAsync();
        }

        public async Task<BlogPost> GetByIdAsync(int id)
        {
            return await _context.BlogPosts.FindAsync(id);
        }

        public async Task AddAsync(BlogPost blogPost)
        {
            await _context.BlogPosts.AddAsync(blogPost);
        }

        public void Update(BlogPost blogPost)
        {
            _context.BlogPosts.Update(blogPost);
        }

        public IEnumerable<BlogPost> GetAll()
        {
            return _context.BlogPosts;
        }

        public BlogPost GetById(int id)
        {
            return _context.BlogPosts.FirstOrDefault(p => p.Id == id);
        }

        public void Add(BlogPost post)
        {
            _context.BlogPosts.Add(post);
        }

        public void Delete(int id)
        {
            var post = GetById(id);
            if (post != null)
            {
                // Usuwamy post
                _context.BlogPosts.Remove(post);

                // Usuwamy powiązane obiekty (tagi, obrazy, komentarze), jeśli nie są powiązane z innymi postami
                foreach (var tag in post.Tags)
                {
                    if (tag.BlogPosts.Count == 1) // Jeśli tag jest powiązany tylko z tym postem
                    {
                        _context.Tags.Remove(tag);
                    }
                }

                foreach (var image in post.Images)
                {
                    if (image.BlogPosts.Count == 1) // Jeśli obraz jest powiązany tylko z tym postem
                    {
                        _context.Images.Remove(image);
                    }
                }

                // Komentarze zawsze są powiązane z jednym postem, więc możemy je bezpiecznie usunąć
                _context.Comments.RemoveRange(post.Comments);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }

}
