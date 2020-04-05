using System;
using System.Collections.Generic;

namespace AnparMobileBK.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime FinishDate { get; set; }
        public string Measure { get; set; }
        public string ProjectNevi { get; set; }


        public List<Photo> Photos { get; set; }
    }
}