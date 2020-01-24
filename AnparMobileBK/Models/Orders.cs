using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnparMobileBK.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int CustomerId { get; set; }
        public int OrderNumber { get; set; }
        public int Discount { get; set; }
    }
}
