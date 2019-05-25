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
    public class CorporateController : ControllerBase
    {

      private IAppRepository _appRepository;

        public CorporateController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }
        [HttpGet]
        public IActionResult GetCorporate()
        {
            var corporate=_appRepository.GetCorporate();
            return Ok(corporate);
        }
        [HttpPost]
        public IActionResult AddCorporate(Corporate corporate)
        {
            var corp = _appRepository.GetCorporate();
            _appRepository.Delete(corp);
            _appRepository.SaveAll();
            _appRepository.Add(corporate);
            _appRepository.SaveAll();
            return Ok("İşlem Başarılı");
        }
    }
}