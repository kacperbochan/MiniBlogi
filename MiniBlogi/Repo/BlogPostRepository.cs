using Microsoft.EntityFrameworkCore;
using MiniBlogi.Controllers.Interfaces;
using MiniBlogi.Data;
using MiniBlogi.Models;
using MiniBlogi.Repo;

namespace MiniBlogi.Controllers
{
    public class BlogPostRepository : GenericRepository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(BlogDbContext context) : base(context) { }

        public async Task DeleteAsync(int id)
        {
            var post = await GetByIdAsync(id);
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
    }

}
