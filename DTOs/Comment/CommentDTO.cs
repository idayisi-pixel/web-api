using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.DTOs.Stock;
using TodoApi.Models;

namespace TodoApi.DTOs.Comment
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public CommentStockDTO PurchasedStock { get; set; } = null!;
        public CommentUserDTO CreatedBy {get; set;} = null!;
        public DateTime CommentDate {get; set;}
    }
}