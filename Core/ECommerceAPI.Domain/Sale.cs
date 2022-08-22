using ECommerceAPI.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain
{
    public class Sale : BaseEntity
    {
        public string Segment { get; set; }
        public string Country { get; set; }
        public string Product { get; set; }
        public string DiscountBand { get; set; }
        public double UnitsSold { get; set; }
        public double ManufactoringSold { get; set; }
        public double SalePrice { get; set; }
        public double GrossSales { get; set; }
        public double Discounts { get; set; }//hansi uucn cixdi xeta?
        public double Sales { get; set; }
        public double COGS { get; set; }
        public double Profit { get; set; }
        public DateTime Date { get; set; }

    }
}
