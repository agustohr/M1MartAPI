using M1MartAPI.Categories.CategoryDtos;
using M1MartBusiness.Interfaces;
using M1MartDataAccess.Models;

namespace M1MartAPI.Categories
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<CategoryDto> GetAllCategories()
        {
            var categories = _categoryRepository.GetAll().Select(c => new CategoryDto()
            {
                Id = c.Id,
                Name = c.Name
            });
            return categories.ToList();
        }

        public CategoryDto GetCategoryById(int id)
        {
            try {
                var category = _categoryRepository.GetByID(id);
                return new CategoryDto()
                {
                    Id = category.Id,
                    Name = category.Name
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CategoryDto CreateCategory(CategoryUpsertDto dto)
        {
            var category = new Category()
            {
                Name = dto.Name,
            };

            var createdCategory = _categoryRepository.Add(category);
            return new CategoryDto()
            {
                Id = createdCategory.Id,
                Name = createdCategory.Name
            };
        }

        public CategoryDto UpdateCategory(int id, CategoryUpsertDto dto)
        {
            try {
                var category = _categoryRepository.GetByID(id);
                category.Name = dto.Name;

                var updatedCategory = _categoryRepository.Update(category);
                return new CategoryDto()
                {
                    Id = updatedCategory.Id,
                    Name = updatedCategory.Name
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteCategory(int id)
        {
            try {
                return _categoryRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
