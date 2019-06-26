using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
        public IActionResult AddTechnicalInfo(TechnicalInfo info)
        {
            string image = null;

            string result = Regex.Replace(info.url, "^data:image/[a-zA-Z]+;base64,", string.Empty);
            if (info.url!= null)
            {
                image = $"images/{info.title}_{Guid.NewGuid().ToString()}.jpg";
                var profilePictureFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", image);
                System.IO.File.WriteAllBytes(profilePictureFilePath, Convert.FromBase64String(result));
            }
            info.url = image;
            _appRepository.Add(info);
            _appRepository.SaveAll();
            return Ok("İşlem Başarılı");
        }

        [HttpDelete]

        public IActionResult DeleteInfo(int id)
        {
            bool info = _appRepository.DeleteInfo(id);
            return Ok(info);
        }
    }
}