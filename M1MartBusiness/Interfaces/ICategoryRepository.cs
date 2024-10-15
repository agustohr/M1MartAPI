using M1MartDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M1MartBusiness.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category GetByID(int id);
        Category Add(Category category);
        Category Update(Category category);
        bool Delete(int id);
        int CountCategory();
    }
}
