using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AnparMobileBK.Utils;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AnparMobileBK
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DataUtils.ReadCart();
            DataUtils.ReadCategories();
            DataUtils.ReadCustomers();
            DataUtils.ReadOrders();
            DataUtils.ReadPerson();
            DataUtils.ReadProducts();
            DataUtils.ReadPersonOrder();
            DataUtils.ReadProjects();
            DataUtils.ReadPhotos();
            DataUtils.ReadCatalogs();
            DataUtils.ReadCorporate();
            DataUtils.ReadCertificates();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}