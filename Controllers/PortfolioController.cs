using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoApi.CustomExceptions;
using TodoApi.DTOs.Portfolio;
using TodoApi.Extensions;
using TodoApi.Interfaces;
using TodoApi.Mappers;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/portfolios")]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioController(
            UserManager<User> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUserName();

            var user = await _userManager.FindByNameAsync(username);

            var portfolios = await _portfolioRepository.GetUserPortfolio(user ?? throw new Exception("User not defined"));

            var userPortfolios = portfolios.Select(portfolio => portfolio.ToPortfolioDto()).ToList();

            return Ok(userPortfolios);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUserPortfolio([FromBody] CreatePortfolioDto portfolioDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var email = User.GetEmail();
            var user = await _userManager.FindByEmailAsync(email);
            var stock = await _stockRepository.GetStockByIdAsync(portfolioDto.StockId);

            if (stock == null) return NotFound("Stock not found");

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(user ?? throw new Exception("User not defined"));

            if (userPortfolio.Any(stk => stk.Stock.Id == stock.Id))
                return BadRequest("Stock added to this portfolio already");

            var portfolio = new Portfolio
            {
                UserId = user.Id,
                StockId = stock.Id
            };

            var createdPortfolio = await _portfolioRepository.CreatePortfolio(portfolio);

            return Ok(createdPortfolio.ToPortfolioDto());
        }


        [HttpDelete("{stockId:int}")]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio([FromRoute] int stockId)
        {
            var email = User.GetEmail();
            var user = await _userManager.FindByEmailAsync(email);

            try
            {
                await _portfolioRepository.DeletePortfolio(stockId, user);

                return NoContent();
            }
            catch(NotFoundException notFoundError)
            {
                return NotFound(new {error = notFoundError.Message});
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}