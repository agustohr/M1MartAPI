﻿using System;
using System.Collections.Generic;

namespace M1MartDataAccess.Models
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = null!;
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual Order InvoiceNumberNavigation { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
