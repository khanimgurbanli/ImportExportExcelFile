using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Common
{
    public class BaseEntity
    {
        /*   It is sufficient for the class to inherit from that class to get the   
         *   properties that are re-declared in each class.
         */
        [Key]
        public int Id { get; set; }
        //public DateTime CreateDate { get; set; }
        //public DateTime  UpdateDate { get; set; }
    }
}
