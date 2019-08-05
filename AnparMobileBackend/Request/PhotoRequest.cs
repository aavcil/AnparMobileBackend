using System;
using System.Security.AccessControl;

namespace AnparMobileBackend.Request
{
    public class PhotoRequest
    {
        public int id { get; set; }
        public string url { get; set; }
        public int projectId { get; set; }
        public bool isMain { get; set; }
    }
}
