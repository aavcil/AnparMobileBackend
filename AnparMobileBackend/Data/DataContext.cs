using AnparMobileBackend.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnparMobileBackend.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Project> Projects{ get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts{ get; set; }
        public DbSet<Photo> Photos { get; set; }

        public DbSet<TechnicalInfo> TechnicalInfos { get; set; }
        public DbSet<Certificate> Certificates { get; set; }

        public DbSet<Corporate> Corporates { get; set; }








    }
}
