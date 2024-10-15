namespace M1MartAPI.Carts.CartDtos
{
    public class CartDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string BuyerUsername { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
