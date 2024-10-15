using M1MartAPI.Categories.CategoryDtos;
using M1MartAPI.Shared;
using M1MartDataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace M1MartAPI.Categories
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            try {
                var categories = _categoryService.GetAllCategories();
                return Ok(new ResponseDto<List<CategoryDto>>()
                {
                    Status = "SUCCESS",
                    Message = $"You've received {categories.Count()} categories.",
                    Data = categories
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<string>()
                {
                    Status = "SERVER ERROR",
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            try {
                var category = _categoryService.GetCategoryById(id);
                return Ok(new ResponseDto<CategoryDto>() {
                    Status = "SUCCESS",
                    Message = $"Here you've a single category requested for id {id}.",
                    Data = category 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<string>()
                {
                    Status = "SERVER ERROR",
                    Message = ex.Message
                });
            }
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody] CategoryUpsertDto dto)
        {
            try {
                if (ModelState.IsValid) {
                    var createdCategory = _categoryService.CreateCategory(dto);
                    return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, new ResponseDto<CategoryDto>()
                    {
                        Status = "SUCCESS",
                        Message = $"Category with name {dto.Name} is successfully added, here is the category you sent.",
                        Data = createdCategory
                    });
                }
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ResponseDto<string>()
                {
                    Status = "BAD REQUEST",
                    Message = "Invalid input data.",
                    Errors = errors
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<string>()
                {
                    Status = "SERVER ERROR",
                    Message = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditCategory(int id, [FromBody] CategoryUpsertDto dto) {
            try {
                if (ModelState.IsValid) {
                    var updatedCategory = _categoryService.UpdateCategory(id, dto);
                    return Ok(new ResponseDto<CategoryDto>()
                    {
                        Status = "SUCCESS",
                        Message = $"Category with id {id} has been updated with your provided data.",
                        Data = updatedCategory
                    });
                }
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                return BadRequest(new ResponseDto<string>()
                {
                    Status = "BAD REQUEST",
                    Message = "Invalid input data.",
                    Errors = errors
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<string>()
                {
                    Status = "SERVER ERROR",
                    Message = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id) {
            try {
                var deleted = _categoryService.DeleteCategory(id);
                if (!deleted) return NotFound(new ResponseDto<string>()
                {
                    Status = "NOT FOUND",
                    Message = $"Category with id {id} is not found."
                });
                return Ok(new ResponseDto<string>()
                {
                    Status = "SUCCESS",
                    Message = $"Category with id {id} has been deleted."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<string>()
                {
                    Status = "SERVER ERROR",
                    Message = ex.Message
                });
            }
        }
    }
}
