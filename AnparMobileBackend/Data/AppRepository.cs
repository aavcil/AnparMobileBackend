using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnparMobileBackend.Entities;
using AnparMobileBackend.Request;
using AnparMobileBackend.Response;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using Contact = AnparMobileBackend.Entities.Contact;

namespace AnparMobileBackend.Data
{
    public class AppRepository : IAppRepository
    {
        private readonly DataContext _context;
        public AppRepository(DataContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }


        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public bool DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.id == id);
            Delete(product);
            return SaveAll();
        }

        public bool MoveToTrashMain(int productId)
        {
            var product = _context.Products.FirstOrDefault(x => x.id == productId);
            if (product != null)
            {
                product.isDeleted = !product.isDeleted;
            }
            return SaveAll();
        }

        public bool DeleteInfo(int id)
        {
            var info = _context.TechnicalInfos.FirstOrDefault(x => x.id == id);
            Delete(info);
            return SaveAll();
        }
        public bool DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.id == id);
            Delete(category);
            return SaveAll();
        }
        public bool DeleteProject(int id)
        {
            var photos = _context.Photos.Where(x => x.projectId == id);
            _context.Photos.RemoveRange(photos);
            var project = _context.Projects.FirstOrDefault(x => x.id == id);
            Delete(project);
            return SaveAll();
        }

        public List<Catalog> GetCatalog()
        {
            return _context.Catalogs.ToList();

        }

        public List<CategoryResponse> GetCategories()
        {

            return _context.Categories.Select(x => new CategoryResponse
            {
                categoryName = x.categoryName,
                id = x.id,
                titleId = x.titleId
            }).ToList();

        }

        public List<CategoryResponse> GetCategoriesByTitle(int titleId)
        {
            return _context.Categories.Where(x => x.titleId == titleId).ToList().Select(x => new CategoryResponse
            {
                titleId = x.titleId,
                categoryName = x.categoryName,
                id = x.id
            }).ToList();
        }

        public List<Certificate> GetCertificates()
        {
            return _context.Certificates.ToList();
        }

        public bool MoveProductToTrash(int productId)
        {
            var product = _context.Products.FirstOrDefault(x => x.id == productId);
            product.isDeleted = true;
            return SaveAll();
        }

        public Contact GetContact()
        {
            return _context.Contacts.FirstOrDefault();
        }


        public Corporate GetCorporate()
        {
            return _context.Corporates.FirstOrDefault();

        }

        public List<TechnicalInfo> GetInfos()
        {
            return _context.TechnicalInfos.ToList();
        }

        public List<ProductResponse> GetProducts()
        {
            return _context.Products.Include(a => a.Category).Where(x => !x.isDeleted).ToList().Select(a => new ProductResponse
            {
                categoryId = a.Category.id,
                categoryName = a.Category.categoryName,
                id = a.id,
                titleId = a.titleId,
                measure = a.measure,
                title = a.title,
                url = a.url,
                description = a.description,
                isDeleted = a.isDeleted
            }).ToList();
        }

        public List<ProductResponse> GetProductsByCategory(int id)
        {
            return _context.Products.Where(x => x.categoryId == id).Include(a => a.Category).Where(x => !x.isDeleted).ToList().Select(z => new ProductResponse
            {
                categoryName = z.Category.categoryName,
                categoryId = z.categoryId,
                titleId = z.titleId,
                measure = z.measure,
                title = z.title,
                url = z.url,
                description = z.description,
                id = z.id,
                isDeleted = z.isDeleted
            }).ToList();
        }

        public ProductResponse GetProductsById(int id)
        {
            var product = _context.Products.Include(a => a.Category).FirstOrDefault(x => x.id == id && !x.isDeleted);
            if (product != null)
            {
                var newProduct = new ProductResponse()
                {
                    id = product.id,
                    categoryId = product.Category.id,
                    categoryName = product.Category.categoryName,
                    titleId = product.titleId,
                    measure = product.measure,
                    title = product.title,
                    description = product.description,
                    url = product.url,
                    isDeleted = product.isDeleted
                };
                return newProduct;
            }

            return null;
        }

        public List<ProductResponse> GetProductsByTitle(int titleId)
        {
            var products = _context.Products.Where(x => x.titleId == titleId).Include(a => a.Category).Where(x => !x.isDeleted).ToList().Select(q => new ProductResponse
            {
                categoryId = q.categoryId,
                categoryName = q.Category.categoryName,
                id = q.id,
                titleId = q.titleId,
                measure = q.measure,
                title = q.title,
                description = q.description,
                url = q.url,
                isDeleted = q.isDeleted
            }).ToList();

            return products;
        }

        public List<ProductResponse> GetTrashProducts()
        {
            return _context.Products.Include(a => a.Category).Where(x => x.isDeleted).Select(q =>
                new ProductResponse()
                {
                    categoryId = q.categoryId,
                    categoryName = q.Category.categoryName,
                    id = q.id,
                    titleId = q.titleId,
                    measure = q.measure,
                    title = q.title,
                    description = q.description,
                    url = q.url,
                    isDeleted = q.isDeleted
                }).ToList();
        }


        public List<ProjectResponse> GetProjects()
        {
            var project = _context.Projects.Include(a => a.Photos).Select((x) => new ProjectResponse
            {

                Photos = x.Photos,
                description = x.description,
                id = x.id,
                title = x.title,
                finishDate = x.finishDate,
                location = x.location,
                measure = x.measure,
                projectNevi = x.projectNevi


            }).ToList();
            return project;

        }

        public ProjectResponse GetProjectsById(int id)
        {
            var project = _context.Projects.Where(q => q.id == id).Include(a => a.Photos).Select((x) => new ProjectResponse
            {

                Photos = x.Photos,
                description = x.description,
                id = x.id,
                title = x.title,
                finishDate = x.finishDate,
                location = x.location,
                measure = x.measure,
                projectNevi = x.projectNevi

            }).FirstOrDefault();
            return project;

        }



        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
