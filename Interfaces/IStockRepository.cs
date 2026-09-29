using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApi.DTOs.Stock;
using TodoApi.Helpers;

namespace TodoApi.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllStocksAsync(QueryObject query);

        Task<Stock?> GetStockByIdAsync(int id);

        Task<Stock> CreateStockAsync(Stock stock);

        Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto updateStockRequestDto);

        Task<bool> DeleteStockAsync(int id);

        Task<bool> StockExists(int id);
    }
}