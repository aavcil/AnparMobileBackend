using AnparMobileBackend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnparMobileBackend.Response
{
    public class CategoryResponse
    {
        public int id { get; set; }
        public string categoryName { get; set; }
        public int titleId { get; set; }
    }
}
