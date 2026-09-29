using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Interfaces;
using TodoApi.Models;
using TodoApi.Data;
using Microsoft.EntityFrameworkCore;
using TodoApi.DTOs.Stock;
using TodoApi.Mappers;
using TodoApi.Helpers;
using System.Linq.Dynamic.Core;


namespace TodoApi.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;

        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateStockAsync(Stock stock)
        {
            await _context.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<bool> DeleteStockAsync(int id)
        {
            var stock = await _context.Stock.FirstOrDefaultAsync(s => s.Id == id);
            if (stock == null) return false;

            _context.Stock.Remove(stock);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Stock>> GetAllStocksAsync(QueryObject query)
        {
            var stocks = _context.Stock.Include(s => s.Comments).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Symbol))
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));

            if (!string.IsNullOrWhiteSpace(query.SortBy.ToString()))
            {
                var direction = query.IsDescending ? "descending" : "ascending";
                stocks = stocks.OrderBy($"{query.SortBy} {direction}");
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetStockByIdAsync(int id)
        {
            return await _context.Stock.Include(s => s.Comments).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto updateStockRequestDto)
        {
            var stock = await _context.Stock.FirstOrDefaultAsync(s => s.Id == id);

            if (stock == null) return null;

            var updatedStock = updateStockRequestDto.ToStockFromUpdateRequest(stock);

            await _context.SaveChangesAsync();

            return updatedStock;
        }

        public Task<bool> StockExists(int id)
        {
            return _context.Stock.AnyAsync(s => s.Id == id);
        }
    }
}