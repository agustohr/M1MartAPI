namespace M1MartAPI.Products.ProductDtos
{
    public class ProductUpsertDto
    {
        public string Name { get; set; } = null!;
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int Stock { get; set; }
        public IFormFile? Image { get; set; }
    }
}
