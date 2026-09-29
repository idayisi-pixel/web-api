using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt {get; set; }
        public int? StockId { get; set; }
        public Stock? Stock { get; set; }
        public string UserId {get; set;} = string.Empty;
        public User? User { get; set; }
    }
}