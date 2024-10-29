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
    public class OrderRepository : IOrderRepository
    {
        private readonly M1MartV2Context _context;
        public OrderRepository(M1MartV2Context context)
        {
            _context = context;
        }
        public List<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public List<Order> GetByUsername(string username)
        {
            return _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .Where(o => o.BuyerUsername == username).ToList();
        }

        public Order GetByIN(string invoiceNumber)
        {
            return _context.Orders.Find(invoiceNumber) ?? throw new NullReferenceException($"Order with invoice number {invoiceNumber} is not found!");
        }

        public Order Add(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }

        public int CountOrder()
        {
            return _context.Orders.Count();
        }

        public int CountSpesificInvoiceNumber(string code)
        {
            return _context.Orders.Where(o => o.InvoiceNumber.Contains(code)).Count();
        }

        public decimal GetTotalIncome()
        {
            return _context.Orders.Sum(o => o.TotalPrice);
        }

        public List<Order> GetMonthlySalesByYear(int year)
        {

            return _context.Orders.Where(o => o.OrderDate.Year == year).ToList();
            //foreach (var sales in salesForYear)
            //{
            //    Console.WriteLine(sales);
            //}
            //return salesForYear.ToDictionary(x => x.TotalSalesAmount, x => x.TotalIncome);

            //var fullYearSales = Enumerable.Range(1, 12)
            //    .GroupJoin(
            //        salesForYear,
            //        month => month,
            //        sale => sale.Month,
            //        (month, sales) => new
            //        {
            //            Month = month,
            //            TotalSalesAmount = sales.Any() ? sales.First().TotalSalesAmount : 0,
            //            TotalIncome = sales.Any() ? sales.First().TotalIncome : 0 
            //        }
            //    ).OrderBy(o => o.Month);
        } 

    }
}
