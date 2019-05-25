using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnparMobileBackend.Data;
using AnparMobileBackend.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AnparMobileBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private IAppRepository _appRepository;

        public CatalogController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
              
        }
        [HttpGet]
        public IActionResult GetCatalogs()
        {
            var catalog = _appRepository.GetCatalog();
            return Ok(catalog);
        }

        [HttpPost]
        public IActionResult AddCatalog(Catalog catalog)
        {
            _appRepository.Add(catalog);
            _appRepository.SaveAll();
            return Ok("İşlem Başarılı");
        }
    }
}
