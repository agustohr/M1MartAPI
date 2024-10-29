using M1MartDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M1MartBusiness.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        List<Product> GetByFilter(int pageNumber, int pageSize, string? productName, string? categoryName);
        List<Product> GetNewestProducts();
        List<Product> GetTopBuy();
        Product GetByID(int id);
        Product Add(Product product);
        Product Update(Product product);
        bool Delete(int id);
        int CountProduct();
        int CountProductFiltered(string? productName, string? categoryName);
    }
}
