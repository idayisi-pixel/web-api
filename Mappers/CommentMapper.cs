using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TodoApi.DTOs.Comment;
using TodoApi.DTOs.Stock;
using TodoApi.Models;
//eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InRvYml5MjNAZ21haWwuY29tIiwiZ2l2ZW5fbmFtZSI6Iml5YW51MjMiLCJuYmYiOjE3OTA2ODQ0NTUsImV4cCI6MTc5MDk0MzY1NSwiaWF0IjoxNzkwNjg0NDU1LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo1MjAxIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1MjAxIn0.NGFGwfZItTX_9J-RFrzvTH1qibGuJPIYpcaFzH7jreyYKj_7td-N6vCH6_fNmfhTrM0XAjOzvQZpp61rjVq1bw
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
                PurchasedStock = new CommentStockDTO{
                    Id = comment.Stock.Id,
                    Symbol = comment.Stock.Symbol,
                    CompanyName = comment.Stock.CompanyName,
                    PurchasePrice = comment.Stock.PurchasePrice,
                    LastDividend = comment.Stock.LastDividend,
                    Industry = comment.Stock.Industry,
                    MarketCap = comment.Stock.MarketCap
                },
                CreatedBy = new CommentUserDTO
                {
                    Id = comment.User.Id,
                    UserName = comment.User.UserName,
                    Email = comment.User.Email
                },
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