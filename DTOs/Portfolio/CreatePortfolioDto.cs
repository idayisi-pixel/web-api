using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.DTOs.Portfolio
{
    public class CreatePortfolioDto
    {
        [Required]
        public int StockId {get; set; }
    }
}