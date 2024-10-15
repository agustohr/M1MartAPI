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
    public class CartRepository : ICartRepository
    {
        private readonly M1MartV2Context _context;
        public CartRepository(M1MartV2Context context)
        {
            _context = context;
        }

        public List<Cart> GetAll()
        {
            return _context.Carts.ToList();
        }

        public Cart GetById(int id)
        {
            return _context.Carts.Find(id) ?? throw new NullReferenceException($"Cart with id {id} is not found!");
        }

        //public List<Cart> GetListById(List<int> ids)
        //{
        //    return _context.Carts.Where(c => ids.Contains(c.Id)).ToList();
        //}

        public List<Cart> GetByUsername(string username)
        {
            return _context.Carts.Include(c => c.Product).Where(c => c.BuyerUsername == username).ToList();
        }

        public Cart? GetByProductIdAndUsername(int productId, string username)
        {
            return _context.Carts.Where(c => c.ProductId == productId && c.BuyerUsername == username).FirstOrDefault();
        }

        public Cart Add(Cart cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();
            _context.Entry(cart).Reference(c => c.Product).Load();
            return cart;
        }

        public Cart Update(Cart cart)
        {
            _context.Carts.Update(cart);
            _context.SaveChanges();
            _context.Entry(cart).Reference(c => c.Product).Load();
            return cart;
        }

        public bool Delete(int id)
        {
            var cart = _context.Carts.Find(id);
            if (cart == null) return false;

            _context.Carts.Remove(cart);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteByUsername(string username)
        {
            var carts = GetByUsername(username);
            if (carts == null) return false;
            _context.RemoveRange(carts);
            _context.SaveChanges();
            return true;
        }

        public void DeleteList(List<int> cartIds)
        {
            _context.RemoveRange(_context.Carts.Where(c => cartIds.Contains(c.Id)).ToList());
            _context.SaveChanges();

        }

    }
}
