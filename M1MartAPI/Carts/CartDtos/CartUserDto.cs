namespace M1MartAPI.Carts.CartDtos
{
    public class CartUserDto
    {
        public int Id { get; set; }
        public CartProductDto Product { get; set; } = null!;
        public int Quantity { get; set; }
        public bool Checked { get; set; }
    }
}
