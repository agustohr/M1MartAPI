using M1MartBusiness.Interfaces;
using M1MartDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
