using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.DTOs.Stock;
using TodoApi.Models;

namespace TodoApi.Mappers
{
    public static class StockMapper
    {
        public static StockDTO ToStockDTO(this Stock stock)
        {
            return new StockDTO
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                PurchasePrice = stock.PurchasePrice,
                LastDividend = stock.LastDividend,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap,
                Comments = stock.Comments.Select(c => c.ToCommentDTO()).ToList()
            };
        }

        public static Stock ToStockFromCreateRequest(this CreateStockRequestDto createStockRequestDto)
        {
            return new Stock
            {
                Symbol = createStockRequestDto.Symbol,
                CompanyName = createStockRequestDto.CompanyName,
                PurchasePrice = createStockRequestDto.PurchasePrice,
                LastDividend = createStockRequestDto.LastDividend,
                Industry = createStockRequestDto.Industry,
                MarketCap = createStockRequestDto.MarketCap
            };                
        }

        public static Stock ToStockFromUpdateRequest(this UpdateStockRequestDto updateStockRequestDto, Stock stock)
        {
            stock.Symbol = updateStockRequestDto.Symbol;
            stock.CompanyName = updateStockRequestDto.CompanyName;
            stock.PurchasePrice = updateStockRequestDto.PurchasePrice;
            stock.LastDividend = updateStockRequestDto.LastDividend;
            stock.Industry = updateStockRequestDto.Industry;
            stock.MarketCap = updateStockRequestDto.MarketCap;

            return stock;
        }
    }
}