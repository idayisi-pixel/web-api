using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.DTOs.Portfolio;
using TodoApi.DTOs.Stock;
using TodoApi.Models;

namespace TodoApi.Mappers
{
    public static class PortfolioMapper
    {
        public static PortfolioDto ToPortfolioDto(this Portfolio portfolio)
        {
            return new PortfolioDto
            {
                UserId = portfolio.UserId,
                Stock = new StockDTO
                {
                    Id = portfolio.Stock.Id,
                    Symbol = portfolio.Stock.Symbol,
                    CompanyName = portfolio.Stock.CompanyName,
                    PurchasePrice = portfolio.Stock.PurchasePrice,
                    LastDividend = portfolio.Stock.LastDividend,
                    Industry = portfolio.Stock.Industry,
                    MarketCap = portfolio.Stock.MarketCap
                }
            };
        }
    }
}