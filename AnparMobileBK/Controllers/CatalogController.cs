using System.Collections.Generic;
using AnparMobileBK.Models;
using AnparMobileBK.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AnparMobileBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        public CatalogController()
        {
        }

        [HttpGet]
        public List<Catalog> Catalogs()
        {
            return DataUtils.GetCatalogs();
        }
    }
}