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
    public class TechnicalInfoController : ControllerBase
    {
        private IAppRepository _appRepository;

        public TechnicalInfoController(IAppRepository appRepository)
        {
            _appRepository = appRepository;

        }
        [HttpGet]
        public IActionResult GetTechnicalInfo()
        {
            var info = _appRepository.GetInfos();
            return Ok(info);
        }
        [HttpPost]
        public IActionResult AddTechnicalInfo(TechnicalInfo ınfo)
        {
            _appRepository.Add(ınfo);
            _appRepository.SaveAll();
            return Ok("İşlem Başarılı");
        }
    }
}