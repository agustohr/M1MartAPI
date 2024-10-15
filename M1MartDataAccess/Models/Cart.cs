using System;
using System.Collections.Generic;

namespace M1MartDataAccess.Models
{
    public partial class Cart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string BuyerUsername { get; set; } = null!;
        public int Quantity { get; set; }

        public virtual User BuyerUsernameNavigation { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
