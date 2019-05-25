using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnparMobileBackend.Entities
{
    public class Category
    {
        public int id { get; set; }
        public string categoryName { get; set; }
        public int titleId { get; set; }
    }
}
