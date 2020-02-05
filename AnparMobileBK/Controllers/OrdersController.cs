using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AnparMobileBK.Models;
using AnparMobileBK.Utils;
using IronPdf;
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

        [HttpGet("MakeAnOrder/{customerId}/{discount}/{personId}")]
        public async Task<HttpResponseMessage> MakeAnOrder(int customerId, string discount, int personId)
        {
            var makeAnOrder = new CreateAnOrder();
            await makeAnOrder.CreateOrder(customerId, discount, personId);

            var guid = Guid.NewGuid().ToString();
            HtmlToPdf Renderer = new HtmlToPdf();
            Renderer.RenderHtmlAsPdf(makeAnOrder.GetTemplate()).SaveAs(customerId + personId + guid + ".pdf");
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            response.Content = new StreamContent(new FileStream(customerId + personId + guid + ".pdf", FileMode.Open, FileAccess.Read));
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = customerId + personId + guid + ".pdf";
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            return response;
        }

    }
}