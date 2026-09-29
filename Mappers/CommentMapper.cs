using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TodoApi.DTOs.Comment;
using TodoApi.Models;

namespace TodoApi.Mappers
{
    public static class CommentMapper
    {
        public static CommentDTO ToCommentDTO(this Comment comment)
        {
            return new CommentDTO
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                StockId = comment.StockId,
                UserId = comment.UserId,
                CommentDate = (DateTime)(comment.UpdatedAt == null ? comment.CreatedAt : comment.UpdatedAt)
            };
        }

        public static Comment ToCommentFromCreateRequest(this CreateCommentDTO createCommentDTO, int stockId)
        {
            return new Comment
            {
                Title = createCommentDTO.Title,
                Content = createCommentDTO.Content,
                StockId = stockId
            };
        }

        public static Comment ToCommentFromUpdateRquest(this UpdateCommentDTO updateCommentDTO, Comment comment, User user)
        {
            comment.Title = updateCommentDTO.Title;
            comment.Content = updateCommentDTO.Content;
            comment.UpdatedAt = DateTime.Now;
            comment.UserId = user.Id;

            return comment;
        }
    }
}