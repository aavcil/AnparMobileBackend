using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnparMobileBK.Dto;
using AnparMobileBK.Models;
using AnparMobileBK.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnparMobileBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly DataUtils _dataUtils;


        public CartController(DataUtils dataUtils)
        {
            _dataUtils = dataUtils;
        }

        [HttpGet]
        public List<CartResponse> GetCart()
        {
            _dataUtils.ReadCart();
            var carts = _dataUtils.GetCarts();
            _dataUtils.ReadProducts();
            var products = _dataUtils.GetProducts();
            return carts.Select(a => new CartResponse()
            {
                Id = a.Id,
                Quantity = a.Quantity,
                ProductId = a.ProductId,
                ProductResponse = product(a.Id)
            }).ToList();
        }
        [HttpPost]
        public void AddCart([FromBody] Cart cart)
        {
            _dataUtils.WriteCarts(cart);
        }
        [HttpGet("Finish")]
        public void Order()
        {
            _dataUtils.UpdateEntry();
        }
        [HttpGet("{id}")]
        public void Carts(int id)
        {
            _dataUtils.ReadCart();
            var carts = _dataUtils.GetCarts().Where(a => a.Id != id).ToList();
            Order();
            foreach (var cart in carts)
            {
                _dataUtils.WriteCarts(cart);
            }
        }

        private ProductResponse product(int id)
        {
            _dataUtils.ReadProducts();
            _dataUtils.ReadCategories();
            var prod = _dataUtils.GetProducts().FirstOrDefault(a => a.Id == id);
            var category = _dataUtils.GetCategories().FirstOrDefault(a => a.Id == prod?.CategoryId);
            return new ProductResponse()
            {
                Id = prod.Id,
                Size = prod.Size,
                ModelCode = prod.ModelCode,
                CartonSize = prod.CartonSize,
                CartonAmount = prod.CartonAmount,
                CartonWeight = prod.CartonWeight,
                CartonMeasure = prod.CartonMeasure,
                ListPrice = prod.ListPrice,
                Series = prod.Series,
                Description = prod.Description,
                Price = prod.Price,
                RawMaterials = prod.RawMaterials,
                CartonVolume = prod.CartonVolume,
                ProductMeasure = prod.ProductMeasure,
                CategoryId = category?.Id,
                CategoryName = category?.Name
            };
        }
    }
}