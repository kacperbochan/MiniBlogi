using Microsoft.EntityFrameworkCore;
using MiniBlogi.Data;
using MiniBlogi.Models;
using MiniBlogi.Repo.Interfaces;

namespace MiniBlogi.Repo
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        // Dodatkowe metody specyficzne dla Comment
        public CommentRepository(BlogDbContext context) : base(context)
        {
        }

        public async Task<List<Comment>> GetAllOfBlog(int blogId)
        {
            return _context.Comments.Where(x => x.BlogPostId == blogId).ToList();
        }
    }

}
