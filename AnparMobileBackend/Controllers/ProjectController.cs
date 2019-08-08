using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AnparMobileBackend.Data;
using AnparMobileBackend.Entities;
using AnparMobileBackend.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnparMobileBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IAppRepository _appRepository;
        public ProjectController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        [HttpGet]
        public ActionResult GetProject()
        {
            var project = _appRepository.GetProjects();
            return Ok(project);
        }

        [HttpGet("getProjectsById")]
        public ActionResult GetProjectsById(int id)
        {
            var project = _appRepository.GetProjectsById(id);
            return Ok(project);
        }
        [HttpPost]
        public ActionResult AddProject([FromBody] ProjectRequest project)
        {

            string url = null;

            //string result = Regex.Replace(project.url, "^data:image/[a-zA-Z]+;base64,", string.Empty);
            //if (project.url != null)
            //{
            //    url = $"images/{project.title}_{Guid.NewGuid().ToString()}.jpg";
            //    var profilePictureFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", url);
            //    System.IO.File.WriteAllBytes(profilePictureFilePath, Convert.FromBase64String(result));
            //}

            var p = new Project()
            {
                description = project.description,
                title = project.title,
                finishDate=project.finishDate,
                location=project.location,
                measure=project.measure,
                projectNevi=project.projectNevi,

            };
            _appRepository.Add(p);
            _appRepository.SaveAll();

            return Ok(p);
        }
        [HttpDelete]
        public ActionResult DeleteProject(int id)
        {
            bool response = _appRepository.DeleteProject(id);
            return Ok(response);
        }

        [HttpPost("AddPhoto")]
        public ActionResult AddPhoto([FromBody] PhotoRequest photo)
        {

            string url = null;

            string result = Regex.Replace(photo.url, "^data:image/[a-zA-Z]+;base64,", string.Empty);
            if (photo.url != null)
            {
                url = $"images/{photo.projectId}_{Guid.NewGuid().ToString()}.jpg";
                var profilePictureFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", url);
                System.IO.File.WriteAllBytes(profilePictureFilePath, Convert.FromBase64String(result));
            }
            var p=new Photo();
            if (photo.isMain && _appRepository.isMainExist(photo.projectId)){
                if (_appRepository.DeleteMain(photo.projectId))
                {
                    p.id = photo.id;
                    p.projectId = photo.projectId;
                    p.url = url;
                    p.isMain = photo.isMain;
                }
               
            }
            else
            {
                p.id = photo.id;
                p.projectId = photo.projectId;
                p.url = url;
                p.isMain = photo.isMain;
            }

           
            _appRepository.Add(p);
            _appRepository.SaveAll();

            return Ok(p);
        }

    }
}