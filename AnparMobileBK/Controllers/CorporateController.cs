using AnparMobileBK.Models;
using AnparMobileBK.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AnparMobileBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorporateController : ControllerBase
    {
        [HttpGet]
        public Corporate Corporate()
        {
            return DataUtils.GetCorporate();
        }
    }
}