using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AnparMobileBackend.Data;
using AnparMobileBackend.Entities;
using AnparMobileBackend.Request;
using AnparMobileBackend.Response;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnparMobileBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private IAppRepository _appRepository;
        private IHostingEnvironment _hostingEnvironment;
        public ProductController(IAppRepository appRepository, IHostingEnvironment hostingEnvironment)
        {
            _appRepository = appRepository;
            _hostingEnvironment = hostingEnvironment;

        }

        [HttpPost]
        public ActionResult ProductAdd([FromBody]ProductRequest product)
        {

            string profilePictureUrl = null;

            string result = Regex.Replace(product.productImage, "^data:image/[a-zA-Z]+;base64,", string.Empty);
            if (product.productImage != null)
            {
                profilePictureUrl = $"images/{product.title}_{Guid.NewGuid().ToString()}.jpg";
                var profilePictureFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", profilePictureUrl);
                System.IO.File.WriteAllBytes(profilePictureFilePath, Convert.FromBase64String(result));
            }

            var products = new Product()
                    {
                        categoryId = product.categoryId,
                        titleId = product.titleId,
                        measure = product.measure,
                        title = product.title,
                        url= profilePictureUrl,
                        description=product.description,
                        isDeleted = false
                        
                    };
                    _appRepository.Add(products);
                  _appRepository.SaveAll();
            return Ok(products);

        }
        [HttpDelete]
        public ActionResult ProductsDelete(int id)
        {
            bool response = _appRepository.DeleteProduct(id);
            return Ok(response);

        }
        [HttpDelete("deleteCategory")]
        public ActionResult CategorDelete(int id)
        {
            bool response=false;
            var product = _appRepository.GetProductsByCategory(id);
            if (product == null)
            {
                response = _appRepository.DeleteCategory(id);
                return Ok(response);
            }

            return Ok(StatusCode(204,"Kategoriye Ait Ürünler Olduğu İçin İşlem Gerçekleştirilemedi."));

        }

        [HttpGet]
        public ActionResult Products()
        {
            var products = _appRepository.GetProducts();
            return Ok(products);
        }
        [HttpGet("getProductsById")]
        public ActionResult GetProductsById(int id)
        {
            var products = _appRepository.GetProductsById(id);
            return Ok(products);
        }
        [HttpGet("getProductsByCategory")]
        public ActionResult GetProductsByCategory(int id)
        {
            var products = _appRepository.GetProductsByCategory(id);
            return Ok(products);
        }
        [HttpGet("getTrashProduct")]
        public ActionResult GetTrashProduct()
        {
            var products = _appRepository.GetTrashProducts();
            return Ok(products);
        }
        [HttpGet("MoveTrashOrMain")]
        public ActionResult MoveTrashOrMain(int id)
        {
            var products = _appRepository.MoveToTrashMain(id);
            return Ok(products);
        }

        [HttpGet("getProductsByTitle")]
        public ActionResult GetProductsByTitle(int titleId)
        {
            var products = _appRepository.GetProductsByTitle(titleId);
            return Ok(products);
        }

        [HttpGet("getCategoriesByTitle")]
        public ActionResult GetCategoriesByTitle(int titleId)
        {
            var categories = _appRepository.GetCategoriesByTitle(titleId);
            return Ok(categories);
        }
        [HttpGet("getCategories")]
        public ActionResult GetCategories()
        {
            var categories = _appRepository.GetCategories();
            return Ok(categories);
        }


        [HttpPost("addCategory")]
        public ActionResult CategoryAdd([FromBody]CategoryRequest category)
        {

            var categoryAdd = new Category()
            {
               categoryName=category.categoryName,
               titleId=category.titleId

            };
            _appRepository.Add(categoryAdd);
            _appRepository.SaveAll();
            return Ok(categoryAdd);

        }

    }
}