using Microsoft.EntityFrameworkCore;
using MiniBlogi.Controllers.Interfaces;
using MiniBlogi.Data;
using MiniBlogi.Models;
using MiniBlogi.Repo;

namespace MiniBlogi.Controllers
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly UnitOfWork _unitOfWork;

        public BlogPostRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _unitOfWork.Context.BlogPosts.ToListAsync();
        }

        public async Task<BlogPost> GetByIdAsync(int id)
        {
            return await _unitOfWork.Context.BlogPosts.FindAsync(id);
        }

        public async Task AddAsync(BlogPost blogPost)
        {
            await _unitOfWork.Context.BlogPosts.AddAsync(blogPost);
        }

        public async Task UpdateAsync(BlogPost blogPost)
        {
            _unitOfWork.Context.Entry(blogPost).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var post = await GetByIdAsync(id);
            if (post != null)
            {
                // Usuwamy post
                _unitOfWork.Context.BlogPosts.Remove(post);

                // Usuwamy powiązane obiekty (tagi, obrazy, komentarze), jeśli nie są powiązane z innymi postami
                foreach (var tag in post.Tags)
                {
                    if (tag.BlogPosts.Count == 1) // Jeśli tag jest powiązany tylko z tym postem
                    {
                        _unitOfWork.Context.Tags.Remove(tag);
                    }
                }

                foreach (var image in post.Images)
                {
                    if (image.BlogPosts.Count == 1) // Jeśli obraz jest powiązany tylko z tym postem
                    {
                        _unitOfWork.Context.Images.Remove(image);
                    }
                }

                // Komentarze zawsze są powiązane z jednym postem, więc możemy je bezpiecznie usunąć
                _unitOfWork.Context.Comments.RemoveRange(post.Comments);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _unitOfWork.Context.SaveChangesAsync();
        }
    }

}
