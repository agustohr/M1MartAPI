using M1MartBusiness.Interfaces;
using M1MartDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M1MartBusiness.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly M1MartV2Context _context;
        public UserRepository(M1MartV2Context context) { 
            _context = context; 
        }
        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetByUsername(string username)
        {
            return _context.Users.Find(username) ?? throw new NullReferenceException($"User with username {username} is not found!");
        }

        public User Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return user;
        }

        public bool Delete(string username)
        {
            var user = _context.Users.Find(username);
            if (user == null) return false;

            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }

        public bool CheckUserIsExist(string username)
        {
            return _context.Users.Any(u => u.Username == username);
        }

        public int CountUser()
        {
            return _context.Users.Where(u => u.Role == "customer").Count();
        }

    }
}
