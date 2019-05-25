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
    public class ContactController : ControllerBase
    {
        private IAppRepository _appRepository;
        public ContactController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        [HttpGet]

        public ActionResult GetContact()
        {
            var contact = _appRepository.GetContact();
            return Ok(contact);
        }
        [HttpPost]
        public ActionResult AddContact([FromBody] Contact contact)
        {
            var con = _appRepository.GetContact();
            _appRepository.Delete(con);
            _appRepository.SaveAll();
            _appRepository.Add(contact);
            _appRepository.SaveAll();
            return Ok(contact);
        }
    }
}