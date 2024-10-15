namespace M1MartAPI.Orders.OrderDtos
{
    public class OrderDto
    {
        public string InvoiceNumber { get; set; } = null!;
        public string BuyerUsername { get; set; } = null!;
        public int TotalProduct { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
