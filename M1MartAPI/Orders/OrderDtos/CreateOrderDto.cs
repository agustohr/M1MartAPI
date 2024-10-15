namespace M1MartAPI.Orders.OrderDtos
{
    public class CreateOrderDto
    {
        public string BuyerUsername { get; set; } = null!;
        public CreateOrderDetailDto[] OrderDetails { get; set; } = null!;
        public int TotalProduct { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
