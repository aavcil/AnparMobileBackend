using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnparMobileBackend.Request
{
    public class ProductRequest
    {    
        public string title { get; set; }
        public string measure { get; set; }
        public string productImage { get; set; }
        public int categoryId { get; set; }
        public int titleId { get; set; }
        public string description { get; set; }
    }
}
