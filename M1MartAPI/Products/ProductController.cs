using M1MartAPI.Categories.CategoryDtos;
using M1MartAPI.Products.ProductDtos;
using M1MartAPI.Shared;
using Microsoft.AspNetCore.Mvc;

namespace M1MartAPI.Products
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("filter")]
        public IActionResult GetProducts(string productName, string categoryName, int pageNumber = 1)
        {
            try
            {
                var products = _productService.GetAllProducts(pageNumber, productName, categoryName);
                return Ok(new ResponseDto<PaginationDto<ProductDto>>()
                {
                    Status = "SUCCESS",
                    Message = $"You've received {products.Data.Count()} products.",
                    Data = products
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

        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                var products = _productService.GetAllProducts();
                return Ok(new ResponseDto<List<ProductDto>>()
                {
                    Status = "SUCCESS",
                    Message = $"You've received {products.Count()} categories.",
                    Data = products
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
        public IActionResult GetProduct(int id)
        {
            try {
                var product = _productService.GetById(id);
                return Ok(new ResponseDto<ProductDto>(){
                    Status = "SUCCESS",
                    Message = $"Here you've a single product requested for id {id}.",
                    Data = product
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
        public IActionResult AddProduct([FromForm] ProductUpsertDto dto)
        {
            try {
                if (ModelState.IsValid) {
                    var createdProduct = _productService.CreateProduct(dto);
                    return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, new ResponseDto<ProductDto>()
                    {
                        Status = "SUCCESS",
                        Message = $"Product with name {dto.Name} is successfully added, here is the product you sent.",
                        Data = createdProduct
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
        public IActionResult UpdateProduct(int id, [FromForm] ProductUpsertDto dto)
        {
            try {
                if (ModelState.IsValid) {
                    var updatedProduct = _productService.UpdateProduct(id, dto);
                    return Ok(new ResponseDto<ProductDto>()
                    {
                        Status = "SUCCESS",
                        Message = $"Product with id {id} has been updated with your provided data.",
                        Data = updatedProduct
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
        public IActionResult DeleteProduct(int id)
        {
            try {
                var deleted = _productService.DeleteProduct(id);
                if(!deleted) return NotFound(new ResponseDto<string>()
                {
                    Status = "NOT FOUND",
                    Message = $"Product with id {id} is not found."
                });
                return Ok(new ResponseDto<string>()
                {
                    Status = "SUCCESS",
                    Message = $"Product with id {id} has been deleted."
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

        [HttpGet("{id}/product-image")]
        public ActionResult<string> GetCover(int id)
        {
            try {
                var product = _productService.GetById(id);
                if (product == null || string.IsNullOrEmpty(product.Image))
                {
                    return NotFound();
                }

                var (imageStream, contentType) = _productService.GetImage(product.Image);

                if (imageStream == null || contentType == null)
                {
                    return NotFound();
                }

                return File(imageStream, contentType);
            }
            catch (Exception ex) {
                return StatusCode(500, new ResponseDto<string>()
                {
                    Status = "SERVER ERROR",
                    Message = ex.Message
                });
            }
        }

        //for customer

    }
}
