using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnparMobileBackend.Response
{
    public class ProductResponse
    {
        public int id { get; set; }
        public string title { get; set; }
        public string measure { get; set; }
        public int categoryId { get; set; }
        public string url { get; set; }
        public int titleId { get; set; }
        public string categoryName { get; set; }
        public string description { get; set; }
        public bool isDeleted { get; set; }
    }
}
