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
    public class CategoryController : ControllerBase
    {
        private readonly DataUtils _dataUtils;


        public CategoryController(DataUtils dataUtils)
        {
            _dataUtils = dataUtils;
        }
        [HttpGet]
        public List<Categories> Category()
        {
            _dataUtils.ReadCategories();
            return _dataUtils.GetCategories();
        }
    }
}