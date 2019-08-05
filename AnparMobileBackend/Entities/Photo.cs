using System;
namespace AnparMobileBackend.Entities
{
    public class Photo
    {
        public int id { get; set; }
        public string url { get; set; }
        public int projectId { get; set; }
        public bool isMain { get; set; }
    }
}
