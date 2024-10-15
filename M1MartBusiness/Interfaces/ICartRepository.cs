using M1MartDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M1MartBusiness.Interfaces
{
    public interface ICartRepository
    {
        List<Cart> GetAll();
        Cart GetById(int id);
        List<Cart> GetByUsername(string username);
        Cart? GetByProductIdAndUsername(int productId, string username);
        Cart Add(Cart cart);
        Cart Update(Cart cart);
        bool Delete(int id);
        bool DeleteByUsername(string username);
        void DeleteList(List<int> cartIds);
    }
}
