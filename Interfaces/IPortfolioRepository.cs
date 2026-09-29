using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.DTOs.Portfolio;
using TodoApi.Models;

namespace TodoApi.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Portfolio>> GetUserPortfolio(User user);

        Task<Portfolio> CreatePortfolio(Portfolio portfolio);

        Task DeletePortfolio(int stockId, User user);
    }
}