using M1MartAPI.Catalog.CatalogDtos;
using M1MartBusiness.Interfaces;

namespace M1MartAPI.Catalog
{
    public class CatalogService
    {
        private readonly IProductRepository _productRepository;
        public CatalogService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
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
    }
}
