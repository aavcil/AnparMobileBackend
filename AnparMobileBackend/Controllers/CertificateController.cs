using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnparMobileBackend.Data;
using AnparMobileBackend.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnparMobileBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private IAppRepository _appRepository;

        public CertificateController(IAppRepository appRepository)
        {
            _appRepository = appRepository;

        }

        [HttpGet]
        public IActionResult GetCertificate()
        {
            var certificates = _appRepository.GetCertificates();
            return Ok(certificates);
        }
        [HttpPost]
        public IActionResult AddCertificate(Certificate certificate)
        {
            _appRepository.Add(certificate);
            _appRepository.SaveAll();
            return Ok("İşlem Başarılı");
        }

    }
}