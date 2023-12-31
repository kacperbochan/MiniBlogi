﻿using MiniBlogi.Models;

namespace MiniBlogi.Repo.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllAsync();
        Task<List<Comment>> GetAllOfBlog(int blogId);
        Task<Comment> GetByIdAsync(int id);
        Task AddAsync(Comment comment);
        Task UpdateAsync(Comment comment);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }

}
