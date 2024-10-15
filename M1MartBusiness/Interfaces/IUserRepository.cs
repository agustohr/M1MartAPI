using M1MartDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M1MartBusiness.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetByUsername(string username);
        User Add(User user);
        User Update(User user);
        bool Delete(string username);
        bool CheckUserIsExist(string username);
        int CountUser();
    }
}
