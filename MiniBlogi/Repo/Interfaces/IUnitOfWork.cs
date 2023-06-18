using MiniBlogi.Controllers.Interfaces;

namespace MiniBlogi.Repo.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBlogPostRepository BlogPostRepository { get; }
        ICommentRepository CommentRepository { get; }
        ITagRepository TagRepository { get; }
        IImageRepository ImageRepository { get; }
        void Save();
    }

}
