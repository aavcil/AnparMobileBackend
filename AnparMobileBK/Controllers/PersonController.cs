﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnparMobileBK.Models;
using AnparMobileBK.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnparMobileBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        public PersonController( )
        {
        }
        [HttpGet]
        public List<Person> Persons()
        {
            return DataUtils.GetPersons();
        }
    }
}