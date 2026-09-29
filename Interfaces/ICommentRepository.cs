using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.DTOs.Comment;
using TodoApi.Models;

namespace TodoApi.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllCommentsAsync();
        Task<Comment?> GetCommentAsync(int id);
        Task<Comment> CreateCommentAsync(int stockId, Comment comment, User user);
        Task<Comment?> UpdateCommentAsync(int id, UpdateCommentDTO updateCommentDTO, User user);
        Task<bool> DeleteCommentAsync(int id, User user);
    }
}