using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.ResponseModels
{
    public class CountryViewModel
    {
        public string Country { get; set; }
        public decimal ProductCount { get; set; }
        public decimal TotalSale { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalProfit { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
    }
}
