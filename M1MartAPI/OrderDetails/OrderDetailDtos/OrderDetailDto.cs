namespace M1MartAPI.OrderDetails.OrderDetailDtos
{
    public class OrderDetailDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
