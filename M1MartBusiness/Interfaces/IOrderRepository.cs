using M1MartDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M1MartBusiness.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetAll();
        List<Order> GetByUsername(string username);
        Order GetByIN(string invoiceNumber);
        Order Add(Order order);
        int CountOrder();
        int CountSpesificInvoiceNumber(string code);
        decimal GetTotalIncome();
        List<Order> GetMonthlySalesByYear(int year);
    }
}
