using M1MartBusiness.Interfaces;
using M1MartDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M1MartBusiness.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly M1MartV2Context _context;
        public CategoryRepository(M1MartV2Context context)
        {
            _context = context;
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category GetByID(int id)
        {
            return _context.Categories.Find(id) ?? throw new NullReferenceException($"Category with id {id} is not found!");
        }

        public Category Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public Category Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
            return category;
        }

        public bool Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if(category == null) return false;

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return true;
        }

        public int CountCategory()
        {
            return _context.Categories.Count();
        }

    }
}
