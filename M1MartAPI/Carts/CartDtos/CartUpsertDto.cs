namespace M1MartAPI.Carts.CartDtos
{
    public class CartUpsertDto
    {
        public int ProductId { get; set; }
        public string BuyerUsername { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
