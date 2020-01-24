using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnparMobileBK.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string CompanyName { get; set; }
        public string CustomerName { get; set; }
        public string TaxNumber { get; set; }
        public string DeliveryType { get; set; }

    }
}
