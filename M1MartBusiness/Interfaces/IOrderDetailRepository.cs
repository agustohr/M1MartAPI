using M1MartDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M1MartBusiness.Interfaces
{
    public interface IOrderDetailRepository
    {
        List<OrderDetail> GetAll();
        List<OrderDetail> AddList(List<OrderDetail> orderDetails);
        OrderDetail Add(OrderDetail orderDetail);
        List<OrderDetail> GetByInvoiceNumber(string invoiceNumber);
    }
}
