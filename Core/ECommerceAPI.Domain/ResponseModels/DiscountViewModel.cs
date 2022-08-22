using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain
{
    public class DiscountViewModel
    {
        public string? Product { get; set; }
        public decimal Discount { get; set; }
        public string? StartDate { get; set;}
        public string? EndDate { get; set; }
    }
}
