using System;
using System.Collections.Generic;

namespace M1MartDataAccess.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public string InvoiceNumber { get; set; } = null!;
        public string BuyerUsername { get; set; } = null!;
        public int TotalProduct { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual User BuyerUsernameNavigation { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
