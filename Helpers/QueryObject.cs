using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace TodoApi.Helpers
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StockSortOption
    {
        Id,
        Symbol,
        CompanyName,
        PurchasePrice,
        MarketPrice
    }

    public class QueryObject
    {
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; } = null;
        public StockSortOption SortBy { get; set; } = StockSortOption.Id;
        public bool IsDescending { get; set; } = false;
        public int PageNumber {get; set; } = 1;
        public int PageSize {get; set; } = 2;
    }
}