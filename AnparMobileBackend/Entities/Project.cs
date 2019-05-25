using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnparMobileBackend.Entities
{
    public class Project
    {
        public int id { get; set; }
        public string title { get; set; }
        public string photosId { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public DateTime finishDate { get; set; }
        public string measure { get; set; }
        public string projectNevi { get; set; }


        public List<Photo> Photos { get; set; }

    }
}
