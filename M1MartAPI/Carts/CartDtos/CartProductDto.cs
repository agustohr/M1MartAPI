namespace M1MartAPI.Carts.CartDtos
{
    public class CartProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
