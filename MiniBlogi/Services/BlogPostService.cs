using MiniBlogi.Controllers.Interfaces;
using MiniBlogi.Models;

namespace MiniBlogi.Services
{
    public class BlogPostService
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public BlogPostService(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        public Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync()
        {
            return _blogPostRepository.GetAllAsync();
        }

        public Task<BlogPost> GetBlogPostByIdAsync(int id)
        {
            return _blogPostRepository.GetByIdAsync(id);
        }

        public Task AddBlogPostAsync(BlogPost blogPost)
        {
            return _blogPostRepository.AddAsync(blogPost);
        }

        public Task UpdateBlogPostAsync(BlogPost blogPost)
        {
            return _blogPostRepository.UpdateAsync(blogPost);
        }

        public Task DeleteBlogPostAsync(int id)
        {
            return _blogPostRepository.DeleteAsync(id);
        }

        public Task SaveChangesAsync()
        {
            return _blogPostRepository.SaveChangesAsync();
        }
    }


}
