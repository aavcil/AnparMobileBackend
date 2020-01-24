using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnparMobileBK.Models;
using AnparMobileBK.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnparMobileBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DataUtils _dataUtils;


        public CustomerController(DataUtils dataUtils)
        {
            _dataUtils = dataUtils;
        }
        [HttpGet]
        public List<Customer> Customer()
        {
            _dataUtils.ReadCustomers();
            return _dataUtils.GetCustomers();
        }
    }
}