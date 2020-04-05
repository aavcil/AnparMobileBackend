using System.Collections.Generic;
using System.Linq;
using AnparMobileBK.Models;
using AnparMobileBK.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AnparMobileBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        public ProjectController()
        {
        }

        [HttpGet]
        public List<Project> Projects()
        {
            var projects = DataUtils.GetProjects().ToList();
            var photos = DataUtils.GetPhotos();
            foreach (var item in projects)
            {
                item.Photos = photos.Where(a => a.ProjectId == item.Id).ToList();
            }

            return projects;
        }

        [HttpGet("{id}")]
        public Project ProjectById(int id)
        {
            var projects = DataUtils.GetProjects().ToList().FirstOrDefault(a => a.Id == id);
            if (projects == null) return null;
            {
                projects.Photos = DataUtils.GetPhotos().Where(a => a.ProjectId == projects.Id).ToList();
                return projects;
            }

        }
    }
}