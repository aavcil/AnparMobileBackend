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
        public CategoryController()
        {
        }

        [HttpGet]
        public List<Categories> Category()
        {
            return DataUtils.GetCategories();
        }

        [HttpGet("{id}")]
        public Categories GetByCategoryId(int id)
        {
            return DataUtils.GetCategories().FirstOrDefault(a => a.Id == id);
        }
    }
}