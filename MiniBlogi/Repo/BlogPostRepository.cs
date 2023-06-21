using Microsoft.EntityFrameworkCore;
using MiniBlogi.Controllers.Interfaces;
using MiniBlogi.Data;
using MiniBlogi.Models;
using MiniBlogi.Repo;
using MiniBlogi.Repo.Interfaces;

namespace MiniBlogi.Controllers
{
    public class BlogPostRepository : GenericRepository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(BlogDbContext context) : base(context) { }

        private readonly int _pageSize = 10;

        public new async Task<BlogPost?> GetByIdAsync(int id)
        {
            return await DbSet
                .Where(x => x.Id == id)
                .Include(x =>x.Tags)
                .Include(x=>x.Images)
                .FirstOrDefaultAsync();
        }

        public new async Task DeleteAsync(int id)
        {
            var post = await GetByIdAsync(id);
            if (post != null)
            {
                // Usuwamy post
                _context.BlogPosts.Remove(post);

                // Usuwamy powiązane obiekty (tagi, obrazy, komentarze), jeśli nie są powiązane z innymi postami
                foreach (var tag in post.Tags)
                {
                    if (_context.Tags.Where(x=>x.Id==tag.Id).Include(x=>x.BlogPosts).FirstOrDefault().BlogPosts.Count== 1) // Jeśli tag jest powiązany tylko z tym postem
                    {
                        _context.Tags.Remove(tag);
                    }
                }

                foreach (var image in post.Images)
                {
                    if (_context.Images.Where(x => x.Id == image.Id).Include(x => x.BlogPosts).FirstOrDefault().BlogPosts.Count == 1) // Jeśli obraz jest powiązany tylko z tym postem
                    {
                        _context.Images.Remove(image);
                    }
                }

                // Komentarze zawsze są powiązane z jednym postem, więc możemy je bezpiecznie usunąć
                _context.Comments.RemoveRange(post.Comments);
            }
        }

        public async Task<bool> IsBlogNotNull()
        {
            return _context.BlogPosts != null;
        }

        public async Task<int> GetPageAmount()
        {
            var peopleCount = await _context.BlogPosts.CountAsync();
            return (int)Math.Ceiling((double)peopleCount / _pageSize);
        }

        public async Task<IEnumerable<BlogPost>> GetCurrentPage(int currentPage)
        {

            if (currentPage < 1) return new List<BlogPost>();
            //pobieramy zapisy z obecnej strony
            return await _context.BlogPosts
                .Include(x=>x.User)
                .Skip((currentPage - 1) * _pageSize)
                .Take(_pageSize).ToListAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetCurrentPageOfUser(int currentPage, string UserId)
        {
            if (currentPage < 1) return new List<BlogPost>();
            //pobieramy zapisy z obecnej strony
            return await _context.BlogPosts
                .Include(x => x.User)
                .Where(x => x.User.Id == UserId)
                .Skip((currentPage - 1) * _pageSize)
                .Take(_pageSize).ToListAsync();
        }

        public async Task<int> GetUserPageAmount(string userId)
        {
            var peopleCount = await _context.BlogPosts.Where(x => x.UserId == userId).CountAsync();
            return (int)Math.Ceiling((double)peopleCount / _pageSize);
        }
    }

}
