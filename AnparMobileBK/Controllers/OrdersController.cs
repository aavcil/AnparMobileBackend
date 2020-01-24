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
    public class OrdersController : ControllerBase
    {
        private readonly DataUtils _dataUtils;

        public OrdersController(DataUtils dataUtils)
        {
            _dataUtils = dataUtils;
        }

        [HttpGet]
        public List<Orders> Orders()
        {
            _dataUtils.ReadOrders();
            return _dataUtils.GetOrders();
        }
        [HttpPost]
        public void WriteOrders([FromBody] Orders orders)
        {
            _dataUtils.WriteOrders(orders);
        }

        [HttpGet("MakeAnOrder/{customerId}/{discount}")]
        public async Task<ActionResult> MakeAnOrder(int customerId, int discount)
        {
            var makeAnOrder = new CreateAnOrder();
            await makeAnOrder.CreateOrder(customerId, discount);
            return Ok(makeAnOrder.GetTemplate());
        }

    }
}