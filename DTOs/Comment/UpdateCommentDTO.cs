using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs.Comment
{
    public class UpdateCommentDTO
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be atleast 5 characters")]
        [MaxLength(20, ErrorMessage = "Title must not exceed 20 characters")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Content must be atleast 5 characters")]
        [MaxLength(280, ErrorMessage = "Content must not exceed 280 characters")]
        public string Content { get; set; } = string.Empty;
    }
}