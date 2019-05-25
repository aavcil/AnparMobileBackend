using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnparMobileBackend.Response
{
    public class Login
    {
        public int Id { get; set; }
        public string tokenString { get; set; }
        public string Username { get; set; }
    }
}
