using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Interfaces;
using TodoApi.DTOs.Comment;
using TodoApi.Mappers;
using TodoApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using TodoApi.Extensions;
using Microsoft.AspNetCore.Identity;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/comments")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        private readonly UserManager<User> _userManager;

        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository, UserManager<User> userManager)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var email = User.GetEmail();

            var user = await _userManager.FindByEmailAsync(email);
            var comments = await _commentRepository.GetAllCommentsAsync(user);
            var response = comments.Select(c => c.ToCommentDTO()).ToList();

            return Ok(response);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            var comment = await _commentRepository.GetCommentAsync(id);

            if (comment == null) return NotFound();

            return Ok(comment.ToCommentDTO());
        }


        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> CreateComment([FromRoute] int stockId, [FromBody] CreateCommentDTO createCommentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var email = User.GetEmail();

            var user = await _userManager.FindByEmailAsync(email);

            /// Check if the Stock exists
            if (!await _stockRepository.StockExists(stockId)) return BadRequest("Stock not found");

            var comment = await _commentRepository.CreateCommentAsync(stockId, createCommentDTO.ToCommentFromCreateRequest(stockId), user);

            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment.ToCommentDTO());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentDTO updateCommentDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var email = User.GetEmail();

            var user = await _userManager.FindByEmailAsync(email);

            var comment = await _commentRepository.UpdateCommentAsync(id, updateCommentDTO, user);

            if (comment == null) return NotFound();

            return Ok(comment.ToCommentDTO());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            var email = User.GetEmail();
            var user = await _userManager.FindByEmailAsync(email);
            var comment = await _commentRepository.DeleteCommentAsync(id, user);

            if (!comment) return NotFound();

            return NoContent();
        }

    }
}