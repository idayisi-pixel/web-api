using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.CustomExceptions;
using TodoApi.Data;
using TodoApi.DTOs.Portfolio;
using TodoApi.Interfaces;
using TodoApi.Mappers;
using TodoApi.Models;

namespace TodoApi.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;
        public PortfolioRepository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public async Task<Portfolio> CreatePortfolio(Portfolio portfolio)
        {
            await _applicationDBContext.Portfolios.AddAsync(portfolio);
            await _applicationDBContext.SaveChangesAsync();

            return await _applicationDBContext.Portfolios
                        .Include(p => p.Stock)
                        .FirstAsync(p =>
                            p.UserId == portfolio.UserId &&
                            p.StockId == portfolio.StockId);
        }

        public async Task<List<Portfolio>> GetUserPortfolio(User user)
        {
            var portfolios = await _applicationDBContext.Portfolios.Where(p => p.UserId == user.Id)
                                    .Include(portfolio => portfolio.Stock)
                                    .ToListAsync();

            return portfolios;
        }

        public async Task DeletePortfolio(int stockId, User user)
        {
            var record = await _applicationDBContext.Portfolios.FirstOrDefaultAsync(p => p.StockId == stockId && p.UserId == user.Id);

            if (record == null)
                throw new NotFoundException("Portfolio not found");

            _applicationDBContext.Portfolios.Remove(record);

            await _applicationDBContext.SaveChangesAsync();
        }
    }
}