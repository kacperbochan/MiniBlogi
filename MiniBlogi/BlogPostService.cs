using MiniBlogi.Controllers.Interfaces;
using MiniBlogi.Models;

namespace MiniBlogi
{
    public class BlogPostService
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public BlogPostService(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        public IEnumerable<BlogPost> GetAllBlogPosts()
        {
            return _blogPostRepository.GetAll();
        }

        public BlogPost GetBlogPostById(int id)
        {
            return _blogPostRepository.GetById(id);
        }

        public void AddBlogPost(BlogPost blogPost)
        {
            // Here we could add some business logic before saving to the database.
            _blogPostRepository.Add(blogPost);
            _blogPostRepository.Save();
        }

        public void UpdateBlogPost(BlogPost blogPost)
        {
            // And also here, for example validating the changes.
            _blogPostRepository.Update(blogPost);
            _blogPostRepository.Save();
        }

        public void DeleteBlogPost(int id)
        {
            _blogPostRepository.Delete(id);
            _blogPostRepository.Save();
        }
    }

}
