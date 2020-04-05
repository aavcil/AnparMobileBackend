using System.Collections.Generic;
using AnparMobileBK.Models;
using AnparMobileBK.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AnparMobileBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        [HttpGet]
        public List<Certificate> Certificates()
        {
            return DataUtils.GetCertificates();
        }
    }
}