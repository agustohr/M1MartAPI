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
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly M1MartV2Context _context;
        public OrderDetailRepository(M1MartV2Context context)
        {
            _context = context;
        }

        public List<OrderDetail> GetAll()
        {
            return _context.OrderDetails.ToList();
        }

        public List<OrderDetail> AddList(List<OrderDetail> orderDetails)
        {
            _context.OrderDetails.AddRange(orderDetails);
            foreach (var orderDetail in orderDetails)
            {
                _context.Entry(orderDetail).Reference(od => od.Product).Load();
                orderDetail.Product.Stock -= orderDetail.Quantity;
            }
            _context.SaveChanges();
            return orderDetails;
        }

        public OrderDetail Add(OrderDetail orderDetail)
        {
            _context.Add(orderDetail);
            _context.SaveChanges();
            return orderDetail;
        }

        public List<OrderDetail> GetByInvoiceNumber(string invoiceNumber)
        {
            return _context.OrderDetails.Include(od => od.Product).Where(od => od.InvoiceNumber == invoiceNumber).ToList();
        }

    }
}
