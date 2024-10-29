using M1MartAPI.Catalog.CatalogDtos;
using M1MartAPI.Shared;
using M1MartBusiness.Interfaces;
using static M1MartAPI.Shared.Constants;

namespace M1MartAPI.Catalog
{
    public class CatalogService
    {
        private readonly IProductRepository _productRepository;
        public CatalogService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public PaginationDto<CatalogDto> GetAllCatalogsFiltered(int pageNumber, string? productName, string? categoryName)
        {
            var products = _productRepository.GetByFilter(pageNumber, PAGE_SIZE, productName, categoryName).Select(p => new CatalogDto()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
            });
            int productsCount = _productRepository.CountProductFiltered(productName, categoryName);
            return new PaginationDto<CatalogDto>()
            {
                Data = products.ToList(),
                PageNumber = pageNumber,
                PageSize = PAGE_SIZE,
                TotalRecords = productsCount
            };
        }

        public List<CatalogDto> GetAllCatalogs()
        {
            try {
                var catalogs = _productRepository.GetAll().Select(p => new CatalogDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                });
                return catalogs.ToList();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public CatalogDetailDto GetCatalogById(int id)
        {
            try {
                var catalog = _productRepository.GetByID(id);
                return new CatalogDetailDto()
                {
                    Id = catalog.Id,
                    Name = catalog.Name,
                    CategoryName = catalog.Category.Name,
                    Price = catalog.Price,
                    Description = catalog.Description,
                    Stock = catalog.Stock
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CatalogDto> GetNewestProducts()
        {
            var products = _productRepository.GetNewestProducts().Select(p => new CatalogDto()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            });
            return products.ToList();
        }

        public List<CatalogDto> GetTopBuy()
        {
            var products = _productRepository.GetTopBuy().Select(p => new CatalogDto()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            });
            return products.ToList();
        }
    }
}
