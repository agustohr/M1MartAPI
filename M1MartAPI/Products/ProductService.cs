using M1MartAPI.Products.ProductDtos;
using M1MartBusiness.Interfaces;
using M1MartBusiness.Repositories;
using M1MartDataAccess.Models;
using M1MartAPI.Categories.CategoryDtos;

namespace M1MartAPI.Products
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly string _imageDirectory;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Assets/Products");
        }

        public List<ProductDto> GetAllProducts()
        {
            var products = _productRepository.GetAll().Select(p => new ProductDto()
            {
                Id = p.Id,
                Name = p.Name,
                Category = new CategoryDto()
                {
                    Id = p.Category.Id,
                    Name = p.Category.Name,
                },
                //CategoryName = p.Category.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                Image = p.Image,
                Discontinue = p.Discontinue,
            });
            return products.ToList();
        }

        public ProductDto GetById(int id)
        {
            try {
                var product = _productRepository.GetByID(id);
                return new ProductDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Category = new CategoryDto()
                    {
                        Id = product.Category.Id,
                        Name = product.Category.Name,
                    },
                    //CategoryName = product.Category.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                    Image = product.Image,
                    Discontinue = product.Discontinue,
                };
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public ProductDto CreateProduct(ProductUpsertDto dto)
        {
            var product = new Product()
            {
                Name = dto.Name,
                CategoryId = dto.CategoryId,
                Price = dto.Price,
                Stock = dto.Stock,
                Description = dto.Description,
                Discontinue = false,
                Image = dto.Image != null ? CreateProductImage(dto.Image) : string.Empty,
            };

            var createdProduct = _productRepository.Add(product);
            return new ProductDto()
            {
                Id = createdProduct.Id,
                Name = createdProduct.Name,
                Category = new CategoryDto()
                {
                    Id= createdProduct.Category.Id,
                    Name = createdProduct.Category.Name,
                },
                //CategoryName = createdProduct.CategoryId.ToString(),
                Description = createdProduct.Description,
                Price = createdProduct.Price,
                Stock = createdProduct.Stock,
                Image = createdProduct.Image,
                Discontinue = createdProduct.Discontinue,
            };
        }

        public ProductDto UpdateProduct(int id, ProductUpsertDto dto)
        {
            try {
                var product = _productRepository.GetByID(id);
                product.Name = dto.Name;
                product.CategoryId = dto.CategoryId;
                product.Price = dto.Price;
                product.Stock = dto.Stock;
                product.Description = dto.Description;

                var updatedProduct = _productRepository.Update(product);
                return new ProductDto()
                {
                    Id = updatedProduct.Id,
                    Name = updatedProduct.Name,
                    Category = new CategoryDto()
                    {
                        Id = updatedProduct.Category.Id,
                        Name = updatedProduct.Category.Name,
                    },
                    //CategoryName = updatedProduct.Category.Name,
                    Description = updatedProduct.Description,
                    Price = updatedProduct.Price,
                    Stock = updatedProduct.Stock,
                    Image = updatedProduct.Image,
                    Discontinue = updatedProduct.Discontinue,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteProduct(int id)
        {
            try
            {
                return _productRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public (FileStream, string) GetImage(string cover)
        {
            var imagePath = Path.Combine(_imageDirectory, cover);

            if (!File.Exists(imagePath))
            {
                return (null, null);
            }

            var imageStream = File.OpenRead(imagePath);
            var contentType = GetImageMimeType(imagePath);

            return (imageStream, contentType);
        }
        //public BookDto UpdateBookCover(int id, IFormFile file)
        //{
        //    var book = _bookRepository.GetById(id);
        //    if (book == null || file == null || file.Length == 0)
        //    {
        //        return null;
        //    }

        //    if (!Directory.Exists(_imageDirectory))
        //    {
        //        Directory.CreateDirectory(_imageDirectory);
        //    }

        //    book.Cover = CreateProductImage(file);
        //    var updatedBook = _bookRepository.Update(book);

        //    return _mapper.Map<BookDto>(updatedBook);
        //}
        private string CreateProductImage(IFormFile file)
        {
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(_imageDirectory, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return fileName;
        }
        private string GetImageMimeType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();

            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                _ => "application/octet-stream",
            };
        }
    }
}
