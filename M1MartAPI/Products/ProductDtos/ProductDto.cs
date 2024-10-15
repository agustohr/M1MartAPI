using M1MartAPI.Categories.CategoryDtos;

namespace M1MartAPI.Products.ProductDtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public CategoryDto Category { get; set; } = null!;
        //public string CategoryName { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int Stock { get; set; }
        public bool Discontinue { get; set; }
        public string? Image { get; set; }
    }
}
