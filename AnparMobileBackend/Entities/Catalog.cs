﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnparMobileBackend.Entities
{
    public class Catalog
    {
        public int id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }

    }
}