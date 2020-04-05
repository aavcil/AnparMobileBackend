using System.Collections.Generic;
using System.Linq;
using AnparMobileBK.Models;
using AnparMobileBK.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AnparMobileBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        public PhotoController()
        {
        }

        [HttpGet]
        public List<Photo> Photos()
        {
            var photos = DataUtils.GetPhotos().ToList();
            return photos;
        }

        [HttpGet("{id}")]
        public List<Photo> PhotosById(int id)
        {
            var photos = DataUtils.GetPhotos().Where(a => a.Id == id).ToList();
            return photos;
        }
    }
}