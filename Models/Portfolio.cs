using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    [Table("Portfolios")]
    public class Portfolio
    {
        public string UserId {get; set;} = string.Empty;
        public int StockId { get; set; }
        public User? User {get; set;}
        public Stock? Stock {get; set;}
    }
}