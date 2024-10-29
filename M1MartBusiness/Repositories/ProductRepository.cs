using M1MartBusiness.Interfaces;
using M1MartDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace M1MartBusiness.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly M1MartV2Context _context;
        public ProductRepository(M1MartV2Context context)
        {
            _context = context;
        }

        public List<Product> GetAll()
        {
            return _context.Products.Include(p => p.Category).ToList();
        }

        public List<Product> GetByFilter(int pageNumber, int pageSize, string? productName, string? categoryName)
        {
            // Normalize empty strings to null
            productName = string.IsNullOrWhiteSpace(productName) ? null : productName;
            categoryName = string.IsNullOrWhiteSpace(categoryName) ? null : categoryName;

            var query = from product in _context.Products.Include(b => b.Category)
                        where
                        (productName == null
                        || product.Name.Contains(productName))
                        &&
                        (categoryName == null
                        || (product.Category != null && product.Category.Name.Contains(categoryName)))
                        select product;

            return query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int CountProductFiltered(string? productName, string? categoryName)
        {
            productName = string.IsNullOrWhiteSpace(productName) ? null : productName;
            categoryName = string.IsNullOrWhiteSpace(categoryName) ? null : categoryName;

            var query = from product in _context.Products.Include(b => b.Category)
                        where
                        (productName == null
                        || product.Name.Contains(productName))
                        &&
                        (categoryName == null
                        || (product.Category != null && product.Category.Name.Contains(categoryName)))
                        select product;

            return query.Count();
        }

        public List<Product> GetNewestProducts()
        {
            return _context.Products
                //.Include(p => p.Category)
                .OrderByDescending(p => p.CreatedDate)
                .Take(5)
                .ToList();
        }

        public List<Product> GetTopBuy()
        {
            return _context.Products
                .Include(p => p.OrderDetails)
                .OrderByDescending(p => p.OrderDetails.Count)
                .Take(5)
                .ToList();
        }

        public Product GetByID(int id)
        {
            return _context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id) ?? throw new NullReferenceException($"Product with id {id} is not found!");
        }

        public Product Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            _context.Entry(product).Reference(p => p.Category).Load();
            return product;
        }

        public Product Update(Product product)
        {
            _context.Update(product);
            _context.SaveChanges();
            _context.Entry(product).Reference(p => p.Category).Load();
            return product;
        }

        public bool Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }

        public int CountProduct()
        {
            return _context.Products.Where(p => p.Discontinue == false).Count();
        }

    }
}
