using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnparMobileBK.Models
{
    public class PersonOrder
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNo { get; set; }
    }
}
