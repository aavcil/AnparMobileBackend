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
    public class ProductController : ControllerBase
    {
        private readonly DataUtils _dataUtils;


        public ProductController(DataUtils dataUtils)
        {
            _dataUtils = dataUtils;
        }
        [HttpGet]
        public List<Products> Get()
        {
            _dataUtils.ReadProducts();
            return _dataUtils.GetProducts();
        }
        [HttpGet("{id}")]
        public ProductResponse GetById(int id)
        {
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