using AnparMobileBackend.Entities;
using AnparMobileBackend.Request;
using AnparMobileBackend.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnparMobileBackend.Data
{
   public interface IAppRepository
    {

        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveAll();
        List<ProductResponse> GetProducts();
        List<CategoryResponse> GetCategories();
        List<CategoryResponse> GetCategoriesByTitle(int titleId);

        ProductResponse GetProductsById(int id);
        List<ProductResponse> GetProductsByCategory(int id);
        List<ProductResponse> GetProductsByTitle(int titleId);
        Contact GetContact();
        Corporate GetCorporate();
        List<Catalog> GetCatalog();
        List<Certificate> GetCertificates();
        List<TechnicalInfo> GetInfos();

        List<ProjectResponse> GetProjects();
        ProjectResponse GetProjectsById(int id);


        bool DeleteProduct(int id);
        bool DeleteCategory(int id);

        bool DeleteProject(int id);



    }
}
