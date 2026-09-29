using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.DTOs.Comment;
using TodoApi.Interfaces;
using TodoApi.Mappers;
using TodoApi.Models;

namespace TodoApi.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateCommentAsync(int stockId, Comment comment, User user)
        {
            comment.UserId = user.Id;
            await _context.Comment.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<bool> DeleteCommentAsync(int id, User user)
        {
            var comment = await _context.Comment.FirstOrDefaultAsync(c => c.Id == id && c.UserId == user.Id);

            if (comment == null) return false;

            _context.Comment.Remove(comment);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _context.Comment.ToListAsync();
        }

        public async Task<Comment?> GetCommentAsync(int id)
        {
            var comment = await _context.Comment.FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null) return null;

            return comment;
        }

        public async Task<Comment?> UpdateCommentAsync(int id, UpdateCommentDTO updateCommentDTO, User user)
        {
            var comment = await _context.Comment.FirstOrDefaultAsync(c => c.Id == id && c.UserId == user.Id);

            if (comment == null) return null;

            Comment updatedComment = updateCommentDTO.ToCommentFromUpdateRquest(comment, user);

            await _context.SaveChangesAsync();

            return comment;
        }
    }
}