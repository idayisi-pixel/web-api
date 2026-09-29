using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.DTOs.Stock;
using TodoApi.Models;

namespace TodoApi.DTOs.Portfolio
{
    public class PortfolioDto
    {
        public string UserId { get; set; }
        public StockDTO Stock { get; set; }
    }
}