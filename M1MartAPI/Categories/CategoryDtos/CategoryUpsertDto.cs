using System.ComponentModel.DataAnnotations;

namespace M1MartAPI.Categories.CategoryDtos
{
    public class CategoryUpsertDto
    {
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "{0} must be filled")]
        public string Name { get; set; } = null!;
    }
}
